/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the
/// back of the queue according to FIFO rules. When GetNextPerson is called, the next
/// person is returned and placed back at the end of the queue if they still have turns.
/// A turns value of 0 or less represents an infinite number of turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and number of turns.
    /// </summary>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Remove and return the next person from the queue.
    /// People with remaining turns are placed at the back of the queue.
    /// A turns value of 0 or less means infinite turns.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        // Zero or negative turns mean infinite turns.
        // Do not modify the Turns value.
        if (person.Turns <= 0)
        {
            _people.Enqueue(person);
        }
        else
        {
            // Use one of the person's finite turns.
            person.Turns--;

            // Only return the person to the queue when turns remain.
            if (person.Turns > 0)
            {
                _people.Enqueue(person);
            }
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}