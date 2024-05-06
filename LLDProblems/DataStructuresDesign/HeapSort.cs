namespace LLDProblems.DataStructuresDesign
{
    // Min Heap
    public class HeapSort
    {
        private int[] _heapArray;
        private int _index;

        public HeapSort(int size)
        {
            _heapArray = new int[size + 1];
            _index = 1;
        }

        public void Insert(int num)
        {
            if (_index == _heapArray.Length)
                return;

            _heapArray[_index] = num;
            Heapyfy(_index / 2, _index);
            ++_index;
        }

        public int Delete()
        {
            if (_index == 0)
                return -1;

            int element = _heapArray[1];
            --_index;
            _heapArray[1] = _heapArray[_index];
            _heapArray[_index] = 0;
            HeapifyRoot(1);
            return element;
        }

        public int Peek()
        {
            if (_index == 0)
                return -1;
            return _heapArray[_index - 1];
        }

        private void Heapyfy(int parent, int index)
        {
            if (parent == 0 || parent == index || _heapArray[parent] < _heapArray[index])
                return;

            Swap(parent, index);
            Heapyfy(parent / 2, parent);
        }

        private void HeapifyRoot(int parent)
        {
            if (parent > _index)
                return;

            int leftChild = 2 * parent < _index ? 2 * parent : -1;
            int rightChild = (2 * parent) + 1 < _index ? (2 * parent) + 1 : -1;
            int selectedInd = 0;

            if (leftChild < 0 && rightChild < 0)
                return;

            if (leftChild > -1 && rightChild > -1)
            {
                if (_heapArray[leftChild] < _heapArray[rightChild])
                    selectedInd = leftChild;
                else
                    selectedInd = rightChild;
            }
            else
            {
                selectedInd = leftChild == -1 ? rightChild : leftChild;
            }

            if (_heapArray[parent] > _heapArray[selectedInd])
            {
                Swap(parent, selectedInd);
                HeapifyRoot(selectedInd);
            }
        }

        private void Swap(int i, int j)
        {
            int tmp = _heapArray[i];
            _heapArray[i] = _heapArray[j];
            _heapArray[j] = tmp;
        }
    }
}
