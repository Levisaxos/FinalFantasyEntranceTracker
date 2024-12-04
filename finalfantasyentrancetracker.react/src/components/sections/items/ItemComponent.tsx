import React, { useState } from 'react';
import MQImageComponentProps from '@/models/props/MQImageProps'

const ItemComponent: React.FC<MQImageComponentProps> = ({ 
  Items
}) => {
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const [isGrayscale, setIsGrayscale] = useState(true);
  const [isHovering, setIsHovering] = useState(false);

  // Handle click event
  const handleImageClick = () => {
    // If currently in grayscale, switch to color
    if (isGrayscale) {
      setIsGrayscale(false);
      return;
    }

    // If in color and there are more images
    if (currentImageIndex < Items.length - 1) {
      setCurrentImageIndex(prev => prev + 1);
    }
  };

  const handleRightClick = (e: React.MouseEvent) => {
    e.preventDefault();
    
    if (!isGrayscale && currentImageIndex == 0) {
      setIsGrayscale(true);
    }

    // If in color and can go backwards
    if (currentImageIndex > 0) {
      setCurrentImageIndex(prev => prev - 1);
    }
  };

  // Determine the current image path and item type
  const currentItem = Items[currentImageIndex];
  const currentImagePath = currentItem.ImagePath;
  const itemType = isGrayscale ? currentItem.ItemType : currentItem.Name;

  return (
    <div 
      className="relative inline-block pb-1"
      onMouseEnter={() => setIsHovering(true)}
      onMouseLeave={() => setIsHovering(false)}
    >
      <img 
        key={`img`}
        src={`/images/items/${currentImagePath}`}
        alt={`Image`}
        className={`w-8 h-8 object-cover ${isGrayscale ? 'grayscale opacity-50' : ''}`}
        onClick={handleImageClick}
        onContextMenu={handleRightClick}
      />
      {isHovering && (
        <div className="absolute left-1/2 transform -translate-x-1/2
          bg-black text-white text-xs px-2 py-1 rounded 
          mb-2 whitespace-nowrap z-10"
        >
          {itemType}
        </div>
      )}
    </div>
  );
}

export default ItemComponent;