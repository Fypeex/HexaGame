using System.Collections.Generic;
using System.Linq;
using Game.Buildings;

namespace Game
{
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