'use client'
import React, { useEffect, useState } from 'react';
import '../css/css.css';

interface Question {
    id: string;
    wording: string;   }

interface QuestionResult {
    question: Question;
    answeredYes: number;
    answeredNo: number;
}

export default function QuestionResults() {
    const [results, setResults] = useState<QuestionResult[]>([]);

    useEffect(() => {
        const fetchResults = async () => {
            try {
                const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Question/results`);
                if (response.ok) {
                    const data : QuestionResult[] = await response.json();
                    setResults(data);
                } else { throw new Error('API Error'); }
            } catch (error) {
                //setResults([
                  //  { wording: "Le TDD est-il indispensable ?", votesOui: 42, votesNon: 8 },
                   // { wording: "Préférez-vous le télétravail ?", votesOui: 150, votesNon: 10 },
                   // { wording: "L'ananas sur la pizza : Oui ou Non ?", votesOui: 30, votesNon: 70 }
              //  ]);
            }
        };

        fetchResults();
        const interval = setInterval(fetchResults, 5000);
        return () => clearInterval(interval);
    }, []);

    const calculatePercentage = (val: number, total: number) => {
        if (total === 0) return 0;
        return Math.round((val / total) * 100);
    };

    return (
        <div className="container">
            <div className="title">DRUPAL</div>
            <h1>Résultats des votes</h1>
            <div className="results-grid">
                {results.map((r) => {
                    const total = r.answeredYes + r.answeredNo;
                    const pctOui = calculatePercentage(r.answeredYes, total);
                    const pctNon = calculatePercentage(r.answeredNo, total);

                    return (
                        <div key={r.question.id} className="result-card">
                            <h2>{r.question.wording}</h2>
                            <div className="stats">
                                <div className="stat-row">
                                    <span className="label">Oui ({r.answeredYes})</span>
                                    <div className="progress-bar">
                                        <div className="fill-oui" style={{ width: `${pctOui}%` }}>{pctOui}%</div>
                                    </div>
                                </div>
                                <div className="stat-row">
                                    <span className="label">Non ({r.answeredNo})</span>
                                    <div className="progress-bar">
                                        <div className="fill-non" style={{ width: `${pctNon}%` }}>{pctNon}%</div>
                                    </div>
                                </div>
                                <p className="total-votes">Total votants: {total}</p>
                            </div>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}