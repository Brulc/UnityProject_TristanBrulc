public class AIPlay: IPlayerController
{
    private Player player;

    public AIPlay(Player player) { this.player = player; }
    public void Play()
    {
        foreach(var card in player.hand )
        {
            var playability = GameManager.Instance.GetCurrentPlayability(card);
            if ( card.type == CardType.Minion && playability[0] )
            {
                if ( card.cost <= player.mana )
                {
                    LaneInfo lane = null;
                    foreach ( var tmpLane in BoardManager.Instance.lanes )
                    {
                        if ( tmpLane.CheckEmpty(card) ) lane = tmpLane;
                    }
                    if ( lane != null )
                        BoardManager.Instance.SpawnMinion(card, lane);
                }
            }
        }
        GameManager.Instance.AdvanceTurn();
    }
}
