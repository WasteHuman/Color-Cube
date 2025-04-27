using System;

namespace Utils
{
    public class Counter
    {
        private int _count;

        public event Action<int> CountChanged;

        public void AddCount()
        {
            _count++;
            CountChanged?.Invoke(_count);
        }
    }
}