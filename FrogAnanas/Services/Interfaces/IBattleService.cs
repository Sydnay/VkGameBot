using FrogAnanas.DTOs;

namespace FrogAnanas.Services
{
    public interface IBattleService
    {
        string Attack(long userId);
        DropInfoDto DropResource(long userId);
        DropInfoDto DropItem(long userId);
        void EndBattle(long userId);
        bool isEnemyDead(long userId);
        bool isPlayerDead(long userId);
    }
}
