
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonControllerDependency : MonoBehaviour
{
    protected Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

}
