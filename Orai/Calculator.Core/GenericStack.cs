using System.Collections;

namespace Calculator.Core;

internal class GenericStack<TValue> : IEnumerable<TValue>
{
    private Node? _top;

    public void Push(TValue value)
    {
        _top = new Node
        {
            Value = value,
            Next = _top
        };
    }

    public TValue Pop()
    {
        if (_top is null)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        TValue value = _top.Value;
        _top = _top.Next;
        return value;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        Node? current = _top;

        while (current is not null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private sealed class Node
    {
        public required TValue Value { get; init; }

        public Node? Next { get; set; }
    }
}
