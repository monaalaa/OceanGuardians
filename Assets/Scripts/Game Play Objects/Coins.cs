using UnityEngine;

public enum Behaviour
{
    Non,
    Rotate,
    SignWave
}
public class Coins : MonoBehaviour
{

    public Behaviour CoinAnimationBehaviour = Behaviour.Non;
    
    [ConditionalField("CoinAnimationBehaviour", Behaviour.Rotate)]
    public int Direction;
    [ConditionalField("CoinAnimationBehaviour", Behaviour.Rotate)]
    public float Speed;
    [ConditionalField("CoinAnimationBehaviour", Behaviour.SignWave)]
    public float Range = 2;

    float period = 2;
    float amplitude = 1;
    float phase = 2f;

    float elapsedTime;
    float angularFrequency;
    float frequency;

    void FixedUpdate()
    {
        Animate();
    }

    void Animate()
    {
        if (CoinAnimationBehaviour == Behaviour.Rotate)
        {
            Rotate();
        }
        else if (CoinAnimationBehaviour == Behaviour.SignWave)
        {
            SineWave();
        }
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Speed * Direction*Time.deltaTime, Space.Self);
    }

    void SineWave()
    {
        //Refrance: https://en.wikipedia.org/wiki/Sine_wave
        
        // y(t) = A * sin(ωt + θ) [Basic Sine Wave Equation]
        // [A = amplitude | ω = AngularFrequency ((2*PI)f) | f = 1/T | T = [period (s)] | θ = phase | t = elapsedTime]

        if (1 / (period) != frequency)
        {
            frequency = 1 / (period);
            angularFrequency = (2 * Mathf.PI) * frequency;
        }
        
        elapsedTime += Time.deltaTime;
        float omegaProduct = (angularFrequency * elapsedTime);
        float y = (amplitude * Mathf.Sin(omegaProduct + phase));

        transform.localPosition = new Vector3(transform.localPosition.x, y*0.2f + Range, transform.localPosition.z);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        gameObject.SetActive(false);
    //        //PlaySound
    //        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Coins");
    //        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
    //        //Increase Score
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //PlaySound
            AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Coins");
            SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
            //Increase Score
            PlayerDataManager.Instance.AddCoin();
        }
    }
}
