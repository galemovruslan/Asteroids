using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover)),
 RequireComponent(typeof(PlayerShooter))]
public class PlayerComposer : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerShooter _shooter;


}
