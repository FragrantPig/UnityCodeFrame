using UnityEngine;

namespace Scripts.Base
{
    public class SafeStack<T>
    {
        private const int DEFAULT_CAPACITY = 1;
        private T[] _value;
        private int _header;
        private int _count;
        private int _capacity;
        private object _lock = new object();
    
        public SafeStack(int nCount)
        {
            if (nCount < DEFAULT_CAPACITY)
            {
                nCount = DEFAULT_CAPACITY;
                Debug.LogWarning($"[SafeStack] SafeStack should not use params lower than {DEFAULT_CAPACITY}");
            }

            _count = 0;
            _capacity = nCount;
            _header = 0;
            _value = new T[nCount];
        }

        public void Push(T param)
        {
            lock (_lock)
            {
                if(_count > 0)
                    _header = RepeatHeader(_header + 1);
                _value[_header] = param;
                _count = Mathf.Min(_count + 1, _capacity);
            }
        }

        public bool TryPop(out T result)
        {
            lock (_lock)
            {
                result = default;
                if (_count == 0)
                    return false;
                result = _value[_header];
                _value[_header] = default;
                _header = RepeatHeader(_header - 1);
                _count = Mathf.Max(_count - 1, _capacity);
                return true;
            }
        }

        public bool TryPeek(out T result)
        {
            lock (_lock)
            {
                result = default;
                if (_count == 0)
                    return false;
                result = _value[_header];
                return true;
            }
        }

        private int RepeatHeader(int nIndex)
        {
            if (_capacity < 1)
                return 0;
            if (nIndex < 0)
                return _capacity + nIndex % _capacity;
            return nIndex % _count;
        }
    }
}