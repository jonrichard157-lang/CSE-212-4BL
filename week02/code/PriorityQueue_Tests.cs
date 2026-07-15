using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities. The item with the
    // highest priority is added last.
    // Expected Result: "Third" should be removed and returned because it has
    // the highest priority. It should no longer appear in the queue.
    // Defect(s) Found: The last item was not checked by the loop, and the
    // dequeued item was returned without being removed from the queue.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 5);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("Third", result);
        Assert.AreEqual("[First (Pri:1), Second (Pri:2)]",
            priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Add multiple items where two items have the same highest priority.
    // Expected Result: "First High" should be removed first because it was added
    // before "Second High", following FIFO rules for equal priorities.
    // Defect(s) Found: The >= comparison selected the last item with the same
    // priority instead of the first item.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("First High", 5);
        priorityQueue.Enqueue("Second High", 5);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("First High", result);
        Assert.AreEqual("[Low (Pri:1), Second High (Pri:5)]",
            priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Add items to the priority queue.
    // Expected Result: Each item should be placed at the back of the queue,
    // regardless of its priority.
    // Defect(s) Found: None. Enqueue correctly added items to the back.
    public void TestPriorityQueue_EnqueueAtBack()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual(
            "[High (Pri:10), Low (Pri:1), Medium (Pri:5)]",
            priorityQueue.ToString()
        );
    }

    [TestMethod]
    // Scenario: Dequeue all items from a queue with different priorities.
    // Expected Result: Items should be returned from highest priority to lowest
    // priority, and the queue should be empty afterward.
    // Defect(s) Found: Dequeue did not remove items from the queue.
    public void TestPriorityQueue_RemoveAllItems()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with
    // the message "The queue is empty."
    // Defect(s) Found: None. The correct exception and message were produced.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );

        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}