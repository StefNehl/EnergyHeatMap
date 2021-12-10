import React, { useEffect, useState } from "react";

interface Props {
    coins: string[]
}

const EHChartCryptoCoinsListContainer: React.FC<Props> = ({ coins }) => {
    return (
        <div>
            {coins.map(c => {
                return (
                    <div key={c}>{c}</div>
                )
            })}
        </div>
    )
}

export default EHChartCryptoCoinsListContainer;