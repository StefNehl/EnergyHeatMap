import React, {useState} from "react";

interface Props {
    coins: string[]
    setCryptoCoinsForFilter: (coins: string[]) => void;
        
}

const EHChartCryptoCoinsListContainer: React.FC<Props> = ({ coins, setCryptoCoinsForFilter }) => {

    var items = coins.map(c => 
    {
        var newItem : [string, boolean] = [c, false];
        return newItem;
    })

    const [selectCoinItems, setSelectCoinItems] = useState<[string, boolean][]>(items);

    let selectCoin = (item: [string, boolean]) =>
    {
        var newItems = selectCoinItems.map(i => 
            {
                if(i[0] === item[0])
                {
                    i[1] = item[1]; 
                }
                return i;
            });
        
        setSelectCoinItems(newItems);
        let coins = selectCoinItems.filter(i => i[1]).map(c => c[0]);
        setCryptoCoinsForFilter(coins);     
    }
    
    return (
        <div>
            {selectCoinItems.map(c => {
                return (   
                    <div key={c[0]}>
                        <input type="checkbox" 
                                id={c[0]} 
                                onChange={(e) => {
                                    selectCoin([e.target.id, e.target.checked]);
                                }}
                                checked={c[1]}/>
                        { "  " + c[0] }
                        <br/>
                    </div>                                                             
                )
            })}
        </div>
    )
}

export default EHChartCryptoCoinsListContainer;