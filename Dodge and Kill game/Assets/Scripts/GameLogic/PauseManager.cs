using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPauseHandler
{
    public bool IsPaused { get; private set; }

    List<IPauseHandler> pauseHandlers = new List<IPauseHandler>();


    public void Subscribe(IPauseHandler handler)
    {
        pauseHandlers.Add(handler);
    }

    public void Unsubscribe(IPauseHandler handler)
    {
        pauseHandlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        foreach (var handler in pauseHandlers)
        {
            handler.SetPaused(isPaused);
        }
    }
}
