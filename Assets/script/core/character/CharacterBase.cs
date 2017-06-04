using UnityEngine;

namespace Assets.script.core.character
{
    public class CharacterBase : MonoBehaviour
    {
        protected float hSpeed;
        protected float vSpeed;

        public bool WarlkingFlg { get; protected set; }
        public bool FreezeFlg { get; set; }

        protected bool collisionFlg;

        public Animator Anim { get; set; }

        public Direction PreDirection { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public enum Direction
        {
            F,
            B,
            L,
            R,
            W
        }

        public CharacterBase()
        {
            CurrentDirection = Direction.W;
            PreDirection = Direction.W;
        }

        public void WalkFront()
        {
            if (!collisionFlg)
            {
                SetWalkValue(0.0f, -1.0f, true, false);
            }
            else
            {
                if (!Anim.GetBool("Fwait"))
                {
                    SetWalkValue(0.0f, -1.0f, true, false);
                }
                else
                {
                    WalkStop();
                }
            }
            PreDirection = CurrentDirection;
            CurrentDirection = Direction.F;
        }

        public void WalkBack()
        {
            if (!collisionFlg)
            {
                SetWalkValue(0.0f, 1.0f, true, false);
            }
            else
            {
                if (!Anim.GetBool("Bwait"))
                {
                    SetWalkValue(0.0f, 1.0f, true, false);
                }
                else
                {
                    WalkStop();
                }
            }
            PreDirection = CurrentDirection;
            CurrentDirection = Direction.B;
        }

        public void WalkLeft()
        {
            if (!collisionFlg)
            {
                SetWalkValue(-1.0f, 0.0f, false, true);
            }
            else
            {
                if (!Anim.GetBool("Lwait"))
                {
                    SetWalkValue(-1.0f, 0.0f, false, true);
                }
                else
                {
                    WalkStop();
                }
            }
            PreDirection = CurrentDirection;
            CurrentDirection = Direction.L;
        }

        public void WalkRight()
        {
            if (!collisionFlg)
            {
                SetWalkValue(1.0f, 0.0f, false, true);
            }
            else
            {
                if (!Anim.GetBool("Rwait"))
                {
                    SetWalkValue(1.0f, 0.0f, false, true);
                }
                else
                {
                    WalkStop();
                }
            }
            PreDirection = CurrentDirection;
            CurrentDirection = Direction.R;
        }

        public void WalkStop()
        {
            WarlkingFlg = false;
            if (vSpeed < 0.0f)
            {
                Anim.SetBool("Fwait", true);
                Anim.SetBool("Bwait", false);
                Anim.SetBool("Lwait", false);
                Anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.F;
            }
            else if (vSpeed > 0.0f)
            {
                Anim.SetBool("Fwait", false);
                Anim.SetBool("Bwait", true);
                Anim.SetBool("Lwait", false);
                Anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.B;
            }
            else if (hSpeed < 0.0f)
            {
                Anim.SetBool("Fwait", false);
                Anim.SetBool("Bwait", false);
                Anim.SetBool("Lwait", true);
                Anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.L;
            }
            else if (hSpeed > 0.0f)
            {
                Anim.SetBool("Fwait", false);
                Anim.SetBool("Bwait", false);
                Anim.SetBool("Lwait", false);
                Anim.SetBool("Rwait", true);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.R;
            }

            hSpeed = 0.0f;
            vSpeed = 0.0f;
            Anim.SetFloat("Hspeed", hSpeed);
            Anim.SetFloat("Vspeed", vSpeed);
            Anim.SetBool("Hstop", true);
            Anim.SetBool("Vstop", true);
        }

        private void SetWalkValue(float hsVal, float vsVal, bool hsFlg, bool vsFlg)
        {
            hSpeed = hsVal;
            vSpeed = vsVal;

            Anim.SetFloat("Hspeed", hSpeed);
            Anim.SetFloat("Vspeed", vSpeed);
            Anim.SetBool("Hstop", hsFlg);
            Anim.SetBool("Vstop", vsFlg);
            WarlkingFlg = true;
            collisionFlg = false;
        }
    }
}
