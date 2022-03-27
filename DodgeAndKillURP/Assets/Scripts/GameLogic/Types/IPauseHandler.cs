using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseHandler
{
    void SetPaused(bool isPaused, bool showPauseUI = true);
}
