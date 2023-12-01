using _Framework;
using _Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Character.Player
{
    public class PlayerAttackRange : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Bot.Bot> onEnemyEnterRange;
        [SerializeField] private UnityEvent<Bot.Bot> onEnemyExitRange;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Bot))
            {
                Debug.Log("Bot enter range");
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);
                bot.ShowTargetIndicator();
                onEnemyEnterRange?.Invoke(bot);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Bot))
            {
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);
                bot.HideTargetIndicator();
                onEnemyExitRange?.Invoke(bot);
            }
        }
    }
}
