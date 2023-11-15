namespace WallyMapSpinzor2;

/// <summary>
/// A bucket-based priority queue. Used to handle drawing order.
/// </summary>
/// <typeparam name="T">The type of elements in the collection</typeparam>
public class BucketPriorityQueue<T>
{
    public List<Queue<T>> Buckets{get; protected set;}
    public int MinBucket{get; protected set;}
    public int MaxBucket{get; protected set;}

    public BucketPriorityQueue(int bucketCount)
    {
        Buckets = new(bucketCount); for(int i = 0; i < bucketCount; ++i) Buckets.Add(new());
        MinBucket = bucketCount; MaxBucket = 0;
    }

    public void Push(T element, int priority)
    {
        if(priority < 0) throw new IndexOutOfRangeException($"Priority {priority} is negative");
        if(priority >= Buckets.Count) throw new IndexOutOfRangeException($"Priority {priority} is bigger than bucket size {Buckets.Count}");
        
        if(priority < MinBucket) MinBucket = priority;
        if(priority > MaxBucket) MaxBucket = priority;

        Buckets[priority].Enqueue(element);
    }

    protected void UpdateMinBucket()
    {
        while(MinBucket < Buckets.Count && Buckets[MinBucket].Count == 0) MinBucket++;
    }

    public T PopMin()
    {
        UpdateMinBucket();
        if(MinBucket >= Buckets.Count) throw new IndexOutOfRangeException("Attempt to pop min from empty bucket queue");
        return Buckets[MinBucket].Dequeue();
    }

    protected void UpdateMaxBucket()
    {
        while(MaxBucket >= 0 && Buckets[MaxBucket].Count == 0) MaxBucket--;
    }

    public T PopMax()
    {
        UpdateMaxBucket();
        if(MaxBucket < 0) throw new IndexOutOfRangeException("Attempt to pop max from empty bucket queue");
        return Buckets[MaxBucket].Dequeue();
    }
}