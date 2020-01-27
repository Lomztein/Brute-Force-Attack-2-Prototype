using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReplaceable
{
    string Identifier { get; }

    void ReplaceWith(object replacement);
}

public interface IReplaceable<T>
{
    string Identifier { get; }

    void ReplaceWith(T replacement);
}