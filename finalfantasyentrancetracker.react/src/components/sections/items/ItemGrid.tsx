import React, { useState, useEffect } from 'react';
import ItemRow from './ItemRow.tsx';
import useApi from '@/utils/ApiFetch.tsx';
import MQItem from '@/models/MQItem';

export const ItemGrid: React.FC = () => {
  const [items, setItems] = useState<MQItem[]>([]);
  const { data: itemData, loading: loadingItems, handleFetch: loadItemApi } = useApi<MQItem[]>();

  const fetchItems = async () => {
    if (items.length === 0 && !loadingItems) {      
      loadItemApi({ url: `${process.env.REACT_APP_API_URL}/api/getItems` });
    }
  };

  useEffect(() => {    
    fetchItems();
  }, []);

  useEffect(() => {
    if (itemData) {
      setItems(itemData);
    }
  }, [itemData]);

  const uniqueRows = Array.from(new Set(items.map(item => item.ItemRow)));
console.log(uniqueRows);
  return (    
    <div className="flex flex-col mt-5 ml-9">
         {uniqueRows.map(rowNumber => (
                <ItemRow 
                    key={rowNumber} 
                    items={items.filter(item => item.ItemRow === rowNumber)} 
                    rowNumber={rowNumber} 
                />
            ))}
    </div>    
  );
};

export default ItemGrid;