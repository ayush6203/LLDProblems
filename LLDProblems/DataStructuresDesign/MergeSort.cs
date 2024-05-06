namespace LLDProblems.DataStructuresDesign
{
    public class MergeSort
    {
        public int[] Sort(int[] arr)
        {
            MergeSorting(0, arr.Length - 1, arr);           // Start splitting first, you need to reduce the range, intial range would be whole array.
            return arr;
        }
        
        private void MergeSorting(int left, int right, int[] arr)
        {
            if(left < right)
            {
                int middle = left + (right - left) / 2;     // Calculate middle point of the range
                MergeSorting(left, middle, arr);            // Split and call seperately the left range  
                MergeSorting(middle + 1, right, arr);       // Split and call seperately the right range

                Merge(left, middle, middle + 1, right, arr);    // Once splitted both, call the merge
            }
        }

        private void Merge(int left1, int right1, int left2, int right2, int[] arr)
        {
            int n1 = right1 - left1 + 1;            // Get the left range
            int n2 = right2 - left2 + 1;            // Get the right range

            int[] mergedArray = new int[n1 + n2];   // Create a temp array which will store sorted data from both left and right range
            int i = left1, j = left2, k = 0;
            while(i <= right1 && j <= right2)       // Sort and store the data by using two pointer
            {
                if (arr[i] < arr[j])
                    mergedArray[k++] = arr[i++];
                else
                    mergedArray[k++] = arr[j++];
            }

            while (i <= right1)
                mergedArray[k++] = arr[i++];

            while (j <= right2)
                mergedArray[k++] = arr[j++];


            int start = Math.Min(left1, left2);     // Push back the data into the main array.
            int end = Math.Max(right1, right2);

            int x = 0;
            for(int w = start; w <= end; w++)
            {
                arr[w] = mergedArray[x++];
            }
        }
    }
}
