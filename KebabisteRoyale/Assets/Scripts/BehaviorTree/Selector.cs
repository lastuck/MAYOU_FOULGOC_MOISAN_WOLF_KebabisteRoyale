using System;
using System.Collections.Generic;
using UnityEngine;

public class Selector<T> : Node<T>
{
    public List<Node<T>> sequences;

    public Selector()
    {
        sequences = new List<Node<T>>();
        nodeType = NodeType.Selector;
    }

    public void AddNode(Node<T> node)
    {
        sequences.Add(node);
    }

    public bool GetSelectorResult()
    {
        foreach (Node<T> node in sequences)
        {
            if (node.nodeType != NodeType.Condition)
            {
                Debug.LogError("Un noeud action ne peut pas être dans un selector");
                return false;
            }

            if (node.CheckCondition())
            {
                return true;
            }
        }

        return false;
    }

    public override bool CheckCondition()
    {
        foreach (Node<T> node in sequences)
        {
            if (node.nodeType != NodeType.Condition)
            {
                Debug.LogError("Un noeud action ne peut pas être dans un selector");
                return false;
            }

            if (node.CheckCondition())
            {
                return true;
            }
        }

        return false;
    }
}
