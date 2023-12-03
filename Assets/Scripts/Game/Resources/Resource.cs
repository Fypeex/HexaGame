using System;
using System.Collections.Generic;
using System.Linq;
using Game.Buildings.Extractors;

namespace Game.Resources
{
    [Serializable]
    public class ResourceConfig
    {
        public int wood;
        public int stone;
        public int iron;
        public int gold;
        public int coal;
        public int oil;
        public int water;
    }

    public class Resource
    {
        private readonly List<Extractor> _extractors;
        private int _amount;
        private int _maxAmount;

        public Resource(Resourcetype type, int amount, int maxAmount, List<Extractor> extractors)
        {
            Type = type;
            _amount = amount;
            _maxAmount = maxAmount;
            _extractors = extractors;
        }

        public Resourcetype Type { get; private set; }

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