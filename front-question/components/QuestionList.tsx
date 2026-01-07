'use client'
import React, { useEffect, useState } from 'react';
import '../css/css.css';

interface Question {
    id: string;
    wording: string;   }


export default function QuestionList() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [userId, setUserId] = useState<string | null>(null);
    const ApiUrl = process.env.NEXT_PUBLIC_API_URL;
    useEffect(() => {
        const initData = async () => {
            const storedId = localStorage.getItem('userId');

            let currentId = storedId;

            if (!currentId) {
                try {
                    const response = await fetch(`${ApiUrl}api/User/Create`, {
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' }
                    });
                    if (response.ok) {
                        const data = await response.json();
                        currentId = data;
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
                    const qResponse = await fetch(`${ApiUrl}/api/Question/unanswered/${currentId}`);
                    if (qResponse.ok) {
                        const qData = await qResponse.json();
                        setQuestions(qData);
                    } else { throw new Error('API Error'); }
                } catch (error) {
                    console.warn("Mode hors ligne: Questions Mock chargées");
                   setQuestions([
                        { id: "guid-quest-1", wording: "Le TDD est-il indispensable ?" },
                        { id: "guid-quest-2", wording: "Préférez-vous le télétravail ?" },
                        { id: "guid-quest-3", wording: "L'ananas sur la pizza : Oui ou Non ?" }
                    ]);
                }
            }
        };

        initData();
    }, []);

    const handleVote = async (questionId: string, answerCode: number) => {
        if (!userId) return;


        try {
            const response = await fetch(`${ApiUrl}/api/Question/Vote`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
               body: JSON.stringify({
                    UserId: userId,
                    QuestionId: questionId,
                    Answer: answerCode
                })
            });

            if (!response.ok) throw new Error('API Error');

            setQuestions((prev) => prev.filter((q) => q.id !== questionId));

        } catch (error) {
            console.warn("Mode hors ligne: Vote simulé");
            setQuestions((prev) => prev.filter((q) => q.id !== questionId));
        }
    };

    if (!userId) return <div>Chargement de l utilisateur</div>;

    return (
        <div className="container">
            <div className="title">DRUPAL</div>
            <h1>Questions disponibles</h1>
            {questions.length === 0 ? (
                <p>Aucune question en attente.</p>
            ) : (
                <ul className="question-list">
                    {questions.map((q) => (
                        <li key={q.id} className="question-item">
                            <span className="question-text">{q.wording}</span>
                            <div className="vote-buttons">
                                <button className="btn-oui" onClick={() => handleVote(q.id, 0)}>🟢 Oui</button>
                                <button className="btn-non" onClick={() => handleVote(q.id, 1)}>🔴 Non</button>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}