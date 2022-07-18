using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacer
{
    private PlayerComposer _player;
    public PlayerPlacer(PlayerComposer player)
    {
        _player = player;
    }


    public void PlaceRandom()
    {
        Rect rect = GetScreenRect();
        Vector2 randomPosition;
        float radius;
        do
        {
            randomPosition = new Vector2(
               Random.Range(rect.x, rect.x + rect.width),
               Random.Range(rect.y, rect.y + rect.height));

            radius = _player.GetComponent<CapsuleCollider>().height / 2f;

        } while (Physics.SphereCast(randomPosition, radius, Vector3.forward, out RaycastHit hit));

        PlaceAt(randomPosition);
    }

    public void PlaceAt(Vector2 location)
    {
        _player.transform.position = location;
    }

    private Rect GetScreenRect()
    {
        Vector3 bottomLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector2 gameFieldSize = new Vector2(
            topRightBoundary.x - bottomLeftBoundary.x,
            topRightBoundary.y - bottomLeftBoundary.y);
        return new Rect(bottomLeftBoundary, gameFieldSize);
    }

}
