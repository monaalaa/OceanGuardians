
using UnityEngine;

public class MoveObject : PlayerActions
{
    public override void DoAction()
    {
        if (!ActionPreformed)
        {
            float distance = (0.5f + ObjectToPreformActionOn.transform.localScale.x / 2f);
            PlayerController.Instance.KeepDistance(distance, ObjectToPreformActionOn);

            PlayerController.Instance.ChangeStatus(CharacterStatus.Pushing);

            ObjectToPreformActionOn.transform.parent = PlayerController.Instance.LocalPlayerInstance.transform;

            PlayerController.Instance.playerAnimator.SetBool("Push", true);
        }

        else
        {
            PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.Grounded;
            ObjectToPreformActionOn.transform.parent = null;
            PlayerController.Instance.playerAnimator.SetBool("Push", false);
        }

        ActionPreformed = !ActionPreformed;
    }
}
