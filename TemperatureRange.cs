using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public class TemperatureRange
    {
        private int _min;
        private int _max;

        public int Min
        {
            get { return _min; }
            private set
            {
                if (value != null)
                {
                    _min = value;
                }
            }
        }
        public int Max
        {
            get { return _max; }
            private set
            {
                if (value != null)
                {
                    _max = value;
                }
            }
        }

        public TemperatureRange(int minimalTemperature, int maximalTemperature)
        {
            if (minimalTemperature < maximalTemperature)
            {
                Min = minimalTemperature;
                Max = maximalTemperature;
            }
            else
            {
                Min = -20;
                Max = 30;
            }
        }
    }
}
