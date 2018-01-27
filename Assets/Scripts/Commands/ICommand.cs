using UnityEngine;

public interface ICommand
{
    void OnCommandEnable();
    void OnCommandDisable();
    void EnterInputVector(Vector2 direction);
}
