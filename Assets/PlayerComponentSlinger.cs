using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static FrontMan;

public class PlayerComponentSlinger : MonoBehaviour
{
    [SerializeField] float _LaunchAngle = 60;
    [SerializeField] float _LaunchForce = 60;
    [SerializeField] float _ReturnSpeed = 3;
    [SerializeField] bool returning = false;
    public List<RobotComponent> AquiredComponents = new List<RobotComponent>();

    [SerializeField] float returnPercent;
    [SerializeField] Vector3 startPoint;
    private void Update()
    {
        if (returning)
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            PlayerMover PMover = FM.Player.GetComponent<PlayerMover>();
            RobotComponent SlungComponent = AquiredComponents[0];

            if (SlungComponent.isAttachedToPlayer)
            {
                SlungComponent.SetKinematic(false).AddForce(new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * _LaunchAngle) * (_LaunchForce * (PMover.isFacingRight ? 1 : -1)),
                    Mathf.Sin(Mathf.Deg2Rad * _LaunchAngle) * _LaunchForce,
                    0f)
                    + PMover.GetComponent<Rigidbody>().velocity,
                    ForceMode.Impulse);
                SlungComponent.GetComponent<Collider>().isTrigger = false;
                SlungComponent.isAttachedToPlayer = false;
            }
            else if (!returning)
            {
                SlungComponent.GetComponent<Collider>().isTrigger = true;
                SlungComponent.GetComponent<TrailRenderer>().enabled = true;
                returning = true;
                startPoint = SlungComponent.transform.position;
                returnPercent = 0;
                DOTween.To(() => returnPercent,
                    (x) =>
                {
                    returnPercent = x;
                    SlungComponent.transform.position = Vector3.Lerp(startPoint, FM.Player.transform.position.SetZ(-.5f), returnPercent);
                }, 1, _ReturnSpeed).OnComplete(() =>
                {
                    SlungComponent.GetComponent<TrailRenderer>().enabled = false;
                    SlungComponent.isAttachedToPlayer = true;
                    returning = false;
                    SlungComponent.SetKinematic(true);
                });


                /*
                _returnTween = SlungComponent.transform.DOMove(FM.Player.transform.position.SetZ(-.5f), _ReturnSpeed);
                
                _returnTween.OnUpdate(() =>
                    {
                        ConsoleProDebug.Watch("Player: ", "" + FM.Player.transform.position.SetZ(-.5f));
                        _returnTween.ChangeEndValue(FM.Player.transform.position.SetZ(-.5f), _returnTween.Duration()-Time.deltaTime, true);
                        ConsoleProDebug.Watch("Endvalue: ", "" + _returnTween.Duration());
                    });*/
            }


        }
    }
}
