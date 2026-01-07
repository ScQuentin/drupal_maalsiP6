'use client'
import React, { useEffect, useState } from 'react';
import '../css/css.css';

interface QuestionResult {
    id: string;
    wording: string;
    votesOui: number;
    votesNon: number;
}

export default function QuestionResults() {
    const [results, setResults] = useState<QuestionResult[]>([]);

    useEffect(() => {
        const fetchResults = async () => {
            try {
                const response = await fetch('http://localhost:5000/api/Question/results');
                if (response.ok) {
                    const data = await response.json();
                    setResults(data);
                } else { throw new Error('API Error'); }
            } catch (error) {
                setResults([
                    { id: "guid-1", wording: "Le TDD est-il indispensable ?", votesOui: 42, votesNon: 8 },
                    { id: "guid-2", wording: "Préférez-vous le télétravail ?", votesOui: 150, votesNon: 10 },
                    { id: "guid-3", wording: "L'ananas sur la pizza : Oui ou Non ?", votesOui: 30, votesNon: 70 }
                ]);
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
            <h1>Résultats des votes</h1>
            <div className="results-grid">
                {results.map((r) => {
                    const total = r.votesOui + r.votesNon;
                    const pctOui = calculatePercentage(r.votesOui, total);
                    const pctNon = calculatePercentage(r.votesNon, total);

                    return (
                        <div key={r.id} className="result-card">
                            <h2>{r.wording}</h2>
                            <div className="stats">
                                <div className="stat-row">
                                    <span className="label">Oui ({r.votesOui})</span>
                                    <div className="progress-bar">
                                        <div className="fill-oui" style={{ width: `${pctOui}%` }}>{pctOui}%</div>
                                    </div>
                                </div>
                                <div className="stat-row">
                                    <span className="label">Non ({r.votesNon})</span>
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