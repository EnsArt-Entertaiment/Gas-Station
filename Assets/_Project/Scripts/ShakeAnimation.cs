using UnityEngine;
namespace _Project.LuckyRoulette.Scripts.Animation
{
    public class ShakeAnimation : MonoBehaviour
    {
        private const int PITCH = 0;
        private const int ROLL = 1;
        private const int YAW = 2;

        [Header("PITCH:0, ROLL:1, YAW:2")] public float[] springConstants;
        [Header("PITCH:0, ROLL:1, YAW:2")] public float[] dragRatios;

        public float pitchCoef = -.1f;
        public float rollCoef = .2f;
        public float yawCoef = .2f;
        public float maxDegreesDelta = 180f;

        private Vector3[] _virtualBall = new Vector3[3];
        private Vector3[] _virtualBallVelocity = new Vector3[3];
        private Vector3[] _virtualBallPrevVelocity = new Vector3[3];

        public Vector2 clampYaw;
        public Vector2 clampRoll;
        public Vector2 clampPitch;

        private void OnEnable()
        {
            _virtualBall[PITCH] = _virtualBall[YAW] = _virtualBall[ROLL] = transform.position;
        }


        private void FixedUpdate()
        {
            for (var i = 0; i < _virtualBall.Length; i++)
            {
                var force = (_virtualBall[i] - transform.position);
                var x = force.magnitude - 0;
                force = force.normalized * (-1 * springConstants[i] * x);
                _virtualBallVelocity[i] += force * Time.fixedDeltaTime;
                _virtualBall[i] += _virtualBallVelocity[i] * Time.fixedDeltaTime;
                _virtualBallVelocity[i] -= _virtualBallVelocity[i] * dragRatios[i] * Time.fixedDeltaTime;
            }

            var ballPitchAcc = (_virtualBallVelocity[PITCH] - _virtualBallPrevVelocity[PITCH]) / Time.fixedDeltaTime;
            var localPitch = transform.InverseTransformDirection(ballPitchAcc);

            var ballRollAcc = (_virtualBallVelocity[ROLL] - _virtualBallPrevVelocity[ROLL]) / Time.fixedDeltaTime;
            var localRoll = transform.InverseTransformDirection(ballRollAcc);

            var ballYawAcc = (_virtualBallVelocity[YAW] - _virtualBallPrevVelocity[YAW]) / Time.fixedDeltaTime;
            var localYaw = transform.InverseTransformDirection(ballYawAcc);

            var targetYaw = Mathf.Clamp(localYaw.x * yawCoef, clampYaw.x, clampYaw.y);
            var targetPitch = Mathf.Clamp(localPitch.z * pitchCoef, clampPitch.x, clampPitch.y);
            var targetRoll = Mathf.Clamp(localRoll.x * rollCoef, clampRoll.x, clampRoll.y);

            var targetRot =
                Quaternion.LookRotation(transform.parent.forward)
                * Quaternion.AngleAxis(targetYaw, Vector3.up)
                * Quaternion.AngleAxis(targetPitch, Vector3.right)
                * Quaternion.AngleAxis(targetRoll, Vector3.forward);


            transform.rotation =
                Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    Time.fixedDeltaTime * maxDegreesDelta
                );

            for (var i = 0; i < _virtualBallPrevVelocity.Length; i++)
            {
                _virtualBallPrevVelocity[i] = _virtualBallVelocity[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_virtualBall[PITCH], 1f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_virtualBall[ROLL], 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_virtualBall[YAW], 1f);
        }
    }
}