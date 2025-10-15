using UnityEngine;

public class ALD : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip MPC;
    void Start()
    {
        MTA();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MTA()
    {
        string PMP = Microphone.devices[0];
        MPC = Microphone.Start(PMP, true, 20, AudioSettings.outputSampleRate);
    }

    public float GLFM()
    {
        return GLFA(Microphone.GetPosition(Microphone.devices[0]), MPC);
    }

    public float GLFA(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if(startPosition < 0)
        {
            return 0;
        }

        float[] wD = new float[sampleWindow];
        clip.GetData(wD, startPosition);

        float tL = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            tL += Mathf.Abs(wD[i]);
        }

        return tL / sampleWindow;
    }
}
