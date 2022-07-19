using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public static Rect ScreenRect {
        get
        {
            if(_screenRect == default)
            {
                _screenRect = GetScreenSize();
            }
            return _screenRect;
        }
    }

    private static Rect _screenRect;

    private static Rect GetScreenSize()
    {
        Vector3 bottomLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector2 gameFieldSize = new Vector2(
            topRightBoundary.x - bottomLeftBoundary.x,
            topRightBoundary.y - bottomLeftBoundary.y);

        Rect screenRect = new Rect(bottomLeftBoundary, gameFieldSize);
        return screenRect;
    }
}
