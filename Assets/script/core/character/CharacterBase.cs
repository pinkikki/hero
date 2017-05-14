using UnityEngine;

namespace script.core.character
{
    public class CharacterBase : MonoBehaviour
    {
        protected float hSpeed;
        protected float vSpeed;

        public bool WarlkingFlg { get; protected set; }
        public bool FreezeFlg { get; set; }

        protected bool collisionFlg;
        protected Animator anim;

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
                if (!anim.GetBool("Fwait"))
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
                if (!anim.GetBool("Bwait"))
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
                if (!anim.GetBool("Lwait"))
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
                if (!anim.GetBool("Rwait"))
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
                anim.SetBool("Fwait", true);
                anim.SetBool("Bwait", false);
                anim.SetBool("Lwait", false);
                anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.F;
            }
            else if (vSpeed > 0.0f)
            {
                anim.SetBool("Fwait", false);
                anim.SetBool("Bwait", true);
                anim.SetBool("Lwait", false);
                anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.B;
            }
            else if (hSpeed < 0.0f)
            {
                anim.SetBool("Fwait", false);
                anim.SetBool("Bwait", false);
                anim.SetBool("Lwait", true);
                anim.SetBool("Rwait", false);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.L;
            }
            else if (hSpeed > 0.0f)
            {
                anim.SetBool("Fwait", false);
                anim.SetBool("Bwait", false);
                anim.SetBool("Lwait", false);
                anim.SetBool("Rwait", true);
                PreDirection = CurrentDirection;
                CurrentDirection = Direction.R;
            }

            hSpeed = 0.0f;
            vSpeed = 0.0f;
            anim.SetFloat("Hspeed", hSpeed);
            anim.SetFloat("Vspeed", vSpeed);
            anim.SetBool("Hstop", true);
            anim.SetBool("Vstop", true);
        }

        private void SetWalkValue(float hsVal, float vsVal, bool hsFlg, bool vsFlg)
        {
            hSpeed = hsVal;
            vSpeed = vsVal;

            anim.SetFloat("Hspeed", hSpeed);
            anim.SetFloat("Vspeed", vSpeed);
            anim.SetBool("Hstop", hsFlg);
            anim.SetBool("Vstop", vsFlg);
            WarlkingFlg = true;
            collisionFlg = false;
        }
    }
}
