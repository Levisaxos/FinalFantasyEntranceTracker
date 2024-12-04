import MQItem from "@/models/MQItem";
import ItemComponent from "./ItemComponent.tsx";
interface ItemRowProps {
    items: MQItem[];
    rowNumber: number;
}

const ItemRow: React.FC<ItemRowProps> = ({ items, rowNumber }) => {
    const itemTypes = Array.from(new Set(items.map(item => item.ItemTypeId)));

    if (itemTypes.length === 0) {
        return null;
    }

    return (
        <div className="flex space-x-4 mb-4">
            {itemTypes.map((itemType) => (              
              <ItemComponent Items={items.filter(item=>item.ItemTypeId == itemType)}/>
            ))}
        </div>
    );
};
export default ItemRow