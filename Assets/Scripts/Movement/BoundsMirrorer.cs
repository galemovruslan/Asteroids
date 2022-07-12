using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsMirrorer 
{
    private Rect _screenRect;

    public BoundsMirrorer(Rect screenRect)
    {
        _screenRect = screenRect;
    }

    // При пересечении границы экрана возвращает "отраженное" 
    // положение возле противодоложной границы
    public Vector2 TryMirrorPosition(Vector2 position)
    {
        if(position.x > _screenRect.x + _screenRect.width)
        {
            position.x = _screenRect.x;
        }
        if(position.x < _screenRect.x)
        {
            position.x = _screenRect.x + _screenRect.width;
        }
        if(position.y > _screenRect.y + _screenRect.height)
        {
            position.y = _screenRect.y;
        }
        if(position.y < _screenRect.y)
        {
            position.y = _screenRect.y + _screenRect.height;
        }
        return position;
    }
}
