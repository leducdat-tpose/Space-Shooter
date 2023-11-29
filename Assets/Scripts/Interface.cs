using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IObstacle
{
    void Move();
    void DeSpawn();
    void outOfBound();
}
interface IBuffItem
{
    void MoveDown();
    void outOfBound();
}

interface IObjectPool
{

}
