using UnityEngine;
using TMPro;

public class PlayerViewHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI jumpsText;
    [SerializeField] private TextMeshProUGUI winsText;
    [SerializeField] private TextMeshProUGUI deathsText;
    [SerializeField] private TextMeshProUGUI orbsCollectedText;
    private TextMeshProUGUI orbsSpentText;
    private TextMeshProUGUI adsWatchedText;
    [SerializeField] private TextMeshProUGUI playTimeText;
    private TextMeshProUGUI avgPlaySessionText;
    [SerializeField] private TextMeshProUGUI distanceClimbedText;
    [SerializeField] private TextMeshProUGUI distanceFallenText;

    public void UpdateView(Player aPlayer)
    {
        idText.text = aPlayer.Id.ToString();
        jumpsText.text = aPlayer.Jumps.ToString();
        winsText.text = aPlayer.Wins.ToString();
        deathsText.text = aPlayer.Deaths.ToString();
        orbsCollectedText.text = aPlayer.OrbsCollected.ToString();
        orbsSpentText.text = aPlayer.OrbsSpent.ToString();
        adsWatchedText.text = aPlayer.AdsWatched.ToString();
        playTimeText.text = aPlayer.PlayTime.ToString();
        avgPlaySessionText.text = aPlayer.AvgPlaySession.ToString();
        distanceClimbedText.text = aPlayer.DistanceClimbed.ToString();
        distanceFallenText.text = aPlayer.DistanceFallen.ToString();
    }
    
}