using System.Collections.Generic;
using System.Linq;
using Game.Buildings.Extractors;

namespace Game.Resources
{
    public class ResourceConfig
    {
        public int wood = 0;
        public int stone = 0;
        public int iron = 0;
        public int gold = 0;
        public int coal = 0;
        public int oil = 0;
        public int water = 0;
    }
    public class Resource
    {
        
        public Resourcetype Type {get; private set;}
        private int _amount;
        private int _maxAmount;
        private readonly List<Extractor> _extractors;
        
        public Resource(Resourcetype type, int amount, int maxAmount, List<Extractor> extractors)
        {
            Type = type;
            _amount = amount;
            _maxAmount = maxAmount;
            _extractors = extractors;
        }
        
        public int Step()
        {
            var amount = _extractors.Sum(extractor => extractor.Extract());
            _amount += amount;
            return amount;
        }
        
        public void AddExtractor(Extractor extractor)
        {
            _extractors.Add(extractor);
        }
    }
    
    
}