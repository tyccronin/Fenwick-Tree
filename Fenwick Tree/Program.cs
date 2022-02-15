/* 
This program demonstrates an implementation of Binary Indexed Tree
- AKA Fenwick Tree.
*/

using System;

public class BinaryIndexedTree {
    // Max tree size
    readonly static int MAX = 1000;
    static int []BITree = new int[MAX];

    // n = Number of elements present in the input array
    // BITree[0..n] = Array which represents Binary Indexed Tree
    // arr[o..n-1] = Input array for which the prefix sum is evaluated

    int getSum(int index){
        // Returns sum of arr[o..index].
        // This function assumes the array is already processed and partial sums of array elements are stored in BITree[].

        int sum = 0; // Initialize result

        // Index in BITree[] is one more than the index in arr[]
        index = index + 1;

        // Traverse ancestors of BITree[index]
        while(index>0){
            sum += BITree[index]; // Add current element of BITree to sum
            index -= index & (-index); // Move index to parent node in getSum View
        }
        return sum;
    }

    public static void updateBIT(int n, int index, int val){
        // Updates a node in BITree at a given index. The given value is added to BITree[i] and all of its ancestors in the tree

        // Index in BITree[] is one more than the index in arr[]
        index = index +1;

        // Traverse all ancestors and add "val"
        while(index <= n){
            BITree[index] += val; // Add "val" to current node of BIT Tree
            index += index & (-index); // Update index to that of parent in update view
        }
    }

    void constructBITree(int[] arr, int n){
        // Function which constructs the Fenwick Tree from a given array

        for(int i = 1; i <= n; i++){
            BITree[i] = 0; // Initialize as 0
        }
        for(int i = 0; i < n; i++){
            updateBIT(n, i, arr[i]);
        }
    }

    // Driver Code
    public static void Main(string[] args){
        int[] freq = {2, 1, 1, 3, 2, 3, 4, 5, 6, 7, 8, 9};
        int n = freq.Length;
        
        BinaryIndexedTree tree = new BinaryIndexedTree();

        tree.constructBITree(freq, n); // Build Fenwick tree from given array

        Console.WriteLine("Sum of elements in arr[0..5]" + " is " + tree.getSum(5));

        freq[3] += 6; // Test update operation
        updateBIT(n, 3, 6); // Update BIT for above change in arr[]
        Console.WriteLine("Sum of elements in arr[0..5]" + " after update is " + tree.getSum(5));
    }
}