namespace LLDProblems.DataStructuresDesign
{
    public class QuickSort
    {
        public int[] Sort(int[] arr)
        {
            QuickSorting(0, arr.Length-1, arr);
            return arr;
        }

        private void QuickSorting(int low, int high, int[] arr)
        {
            if(low < high)
            {
                int p = Partition(low, high, arr);                          // Place the pivot element at the desired place and return its inde
                QuickSorting(low, p - 1, arr);                              // Make Seperate call for left of the pivot
                QuickSorting(p + 1, high, arr);                             // Make Seperate call for right of the pivot
            }
        }

        private int Partition(int low, int high, int[] arr)
        {
            int pivot = arr[high];
            int i = low - 1;

            for(int j = low; j < high; j++)
            {
                if(arr[j] < pivot)
                {
                    ++i;
                    Swap(i, j, arr);
                }
            }

            Swap(i+1, high, arr);
            return i + 1;
        }

        private void Swap(int a, int b, int[] arr)
        {
            if (a >= arr.Length || b < 0)
                return;

            int tmp = arr[a];
            arr[a] = arr[b];
            arr[b] = tmp;
        }
    }
}

/*
 1 3 4 2
       ^
 ^
 */
