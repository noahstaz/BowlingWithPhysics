using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    //A reference to our ballController
    [SerializeField] private BallController ball;
    //A reference for our PinCollection prefab we made in Section 2.2
    [SerializeField] private GameObject pinCollection;
    //A reference for an empty GameObject which we'll
    //use to spawn our pin collection prefab
    [SerializeField] private Transform pinAnchor;
    //A reference for our input manager
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    private FallTrigger[] fallTriggers;
    private FallTrigger[] pins;
    private GameObject pinObjects; // Add this field declaration

    private void Start()
    {
        pins = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in pins)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    private void HandleReset()
    {
        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
    {
        // We first make sure that all the previous pins have been destroyed
        // this is so that we don't create a new collection of
        //standing pins on top of already fallen pins

        if(pinObjects)
        {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
        }

        // We then instatiate a new set of pins to our pin anchor transform
        pinObjects = Instantiate(pinCollection,
                                pinAnchor.transform.position,
                                Quaternion.identity, transform);

        // We add the Increment Score function as a listener to
        // the OnPinFall event each of new pins
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include,
                                                        FindObjectsSortMode.None);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }
}
