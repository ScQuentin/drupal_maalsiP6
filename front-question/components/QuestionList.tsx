'use client'
import React, { useEffect, useState } from 'react';
import '../css/css.css';

interface Question {
    id: number;
    libelle: string;
}

interface User {
    id: number;
}

export default function QuestionList() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [userId, setUserId] = useState<number | null>(null);

    useEffect(() => {
        const initData = async () => {
            const storedId = localStorage.getItem('userId');
            let currentId = storedId ? parseInt(storedId) : null;


            if (!currentId) {
                try {
                    const response = await fetch('/api/Question/user', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' }
                    });
                    if (response.ok) {
                        const data: User = await response.json();
                        currentId = data.id;
                        localStorage.setItem('userId', currentId.toString());
                    } else { throw new Error('API Error'); }
                } catch (error) {
                    console.warn("Mode hors ligne: User Mock généré");
                    currentId = 999; // Mock ID
                    localStorage.setItem('userId', currentId.toString());
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
                        { id: 1, libelle: "Le TDD est-il indispensable ?" },
                        { id: 2, libelle: "Préférez-vous le télétravail ?" },
                        { id: 3, libelle: "L'ananas sur la pizza : Oui ou Non ?" }
                    ]);
                }
            }
        };

        initData();
    }, []);

    const handleVote = async (questionId: number, answer: number) => {
        if (!userId) return;

        try {
            const response = await fetch('/api/Question/vote', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ id_user: userId, id_question: questionId, answer })
            });

            if (!response.ok) throw new Error('API Error');

            setQuestions((prev) => prev.filter((q) => q.id !== questionId));

        } catch (error) {
            console.warn("Mode hors ligne: Vote simulé");
            setQuestions((prev) => prev.filter((q) => q.id !== questionId));
        }
    };

    if (!userId) return <div>Chargement...</div>;

    return (
        <div className="container">
            <h1>Questions disponibles</h1>
            {questions.length === 0 ? (
                <p>Aucune question en attente.</p>
            ) : (
                <ul className="question-list">
                    {questions.map((q) => (
                        <li key={q.id} className="question-item">
                            <span className="question-text">{q.libelle}</span>
                            <div className="vote-buttons">
                                <button className="btn-oui" onClick={() => handleVote(q.id, 1)}>🟢 Oui</button>
                                <button className="btn-non" onClick={() => handleVote(q.id, 2)}>🔴 Non</button>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}