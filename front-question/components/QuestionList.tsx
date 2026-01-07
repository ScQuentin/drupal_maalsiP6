'use client'
import React, { useEffect, useState } from 'react';
import '../css/css.css';

interface Question {
    Id: string;
    Wording: string;   }

interface User {
    Id: string;
}

export default function QuestionList() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [userId, setUserId] = useState<string | null>(null);

    useEffect(() => {
        const initData = async () => {
            const storedId = localStorage.getItem('userId');

            let currentId = storedId;

            if (!currentId) {
                try {
                    const response = await fetch('/api/Question/user', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' }
                    });
                    if (response.ok) {
                        const data: User = await response.json();
                        currentId = data.Id;
                        if(currentId) localStorage.setItem('userId', currentId);
                    } else { throw new Error('API Error'); }
                } catch (error) {
                    console.warn("Mode hors ligne: User Mock généré");
                    currentId = "test-guid-user-001";
                    localStorage.setItem('userId', currentId);
                }
            }
            setUserId(currentId);

            if (currentId) {
                try {
                    const qResponse = await fetch(`/api/Question/unanswered/${currentId}`);
                    if (qResponse.ok) {
                        const qData = await qResponse.json();
                        setQuestions(qData);
                    } else { throw new Error('API Error'); }
                } catch (error) {
                    console.warn("Mode hors ligne: Questions Mock chargées");
                   setQuestions([
                        { Id: "guid-quest-1", Wording: "Le TDD est-il indispensable ?" },
                        { Id: "guid-quest-2", Wording: "Préférez-vous le télétravail ?" },
                        { Id: "guid-quest-3", Wording: "L'ananas sur la pizza : Oui ou Non ?" }
                    ]);
                }
            }
        };

        initData();
    }, []);

    const handleVote = async (questionId: string, answerCode: number) => {
        if (!userId) return;

      const answerText = answerCode === 1 ? "Oui" : "Non";

        try {
            const response = await fetch('/api/Question/Vote', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
               body: JSON.stringify({
                    UserId: userId,
                    QuestionId: questionId,
                    Answer: answerText
                })
            });

            if (!response.ok) throw new Error('API Error');

            setQuestions((prev) => prev.filter((q) => q.Id !== questionId));

        } catch (error) {
            console.warn("Mode hors ligne: Vote simulé");
            setQuestions((prev) => prev.filter((q) => q.Id !== questionId));
        }
    };

    if (!userId) return <div>Chargement de l utilisateur</div>;

    return (
        <div className="container">
            <h1>Questions disponibles</h1>
            {questions.length === 0 ? (
                <p>Aucune question en attente.</p>
            ) : (
                <ul className="question-list">
                    {questions.map((q) => (
                        <li key={q.Id} className="question-item">
                            <span className="question-text">{q.Wording}</span>
                            <div className="vote-buttons">
                                {/* On garde 1 et 2 ici pour la logique UI, converti dans handleVote */}
                                <button className="btn-oui" onClick={() => handleVote(q.Id, 1)}>🟢 Oui</button>
                                <button className="btn-non" onClick={() => handleVote(q.Id, 2)}>🔴 Non</button>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}