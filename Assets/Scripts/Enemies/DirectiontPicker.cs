using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DirectiontPicker
{
    private Rect _screen;

    public DirectiontPicker()
    {
        Vector3 bottomLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector2 gameFieldSize = new Vector2(
            topRightBoundary.x - bottomLeftBoundary.x,
            topRightBoundary.y - bottomLeftBoundary.y);

        _screen = new Rect(bottomLeftBoundary, gameFieldSize);
    }

    public LaunchData GetHorizontalDirection(bool isPositiveDirection, float marginPercent)
    {
        Assert.IsTrue(marginPercent < 0.5f, "Margine percent must be less than 0.5");

        float margineUnits = _screen.height * marginPercent;
        Vector2 direction;
        Vector2 start;
        Vector2 end;

        if (isPositiveDirection)
        {
            start = RandomLeft(margineUnits);
            end = RandomRight(margineUnits);
            direction = (end - start).normalized;
        }
        else
        {
            start = RandomRight(margineUnits);
            end = RandomLeft(margineUnits);
            direction = (end - start).normalized;
        }
        return new LaunchData() { Start = start, Direction = direction };
    }

    public LaunchData GetVerticalDirection(bool isPositiveDirection, float marginPercent)
    {
        Assert.IsTrue(marginPercent < 0.5f, "Margine percent must be less than 0.5");
        float margineUnits = _screen.width * marginPercent;
        Vector2 direction;
        Vector2 start;
        Vector2 end;

        if (isPositiveDirection)
        {
            start = RandomBottom(margineUnits);
            end = RandomTop(margineUnits);
            direction = (end - start).normalized;
        }
        else
        {
            start = RandomTop(margineUnits);
            end = RandomBottom(margineUnits);
            direction = (end - start).normalized;
        }
        return new LaunchData() { Start = start, Direction = direction }; 
    }

    

    private Vector2 RandomLeft(float margin)
    {
        if (margin > _screen.height)
        {
            throw new ArgumentException("Margine can't be bigger than screen height");
        }

        float height = UnityEngine.Random.Range(_screen.y + margin, _screen.y + _screen.height - margin);
        return new Vector2(_screen.x, height);
    }
    private Vector2 RandomRight(float margin)
    {
        if (margin > _screen.height)
        {
            throw new ArgumentException("Margine can't be bigger than screen height");
        }

        float height = UnityEngine.Random.Range(_screen.y + margin, _screen.y + _screen.height - margin);
        return new Vector2(_screen.x + _screen.width, height);
    }

    private Vector2 RandomTop(float margin)
    {
        if (margin > _screen.width)
        {
            throw new ArgumentException("Margine can't be bigger than screen width");
        }

        float width = UnityEngine.Random.Range(_screen.x + margin, _screen.x + _screen.width - margin);
        return new Vector2(width, _screen.y + _screen.height);
    }
    private Vector2 RandomBottom(float margin)
    {
        if (margin > _screen.width)
        {
            throw new ArgumentException("Margine can't be bigger than screen width");
        }

        float width = UnityEngine.Random.Range(_screen.x + margin, _screen.x + _screen.width - margin);
        return new Vector2(width, _screen.y);
    }

}

