using UnityEngine;
using System.Collections;

// 인덱서 확장
public interface Interface_GenericIndexer<ReturnType, IndexType>
{
    ReturnType this[IndexType index]
    { get; set; }
}