using UltEvents;
using UnityEngine;

public class EnemyDedTriger : MonoBehaviour
{
    public SpawnEnemiesTrigger[] NeseserySpanTrigers;

    [BoltsComment("Wen all spesefid enemys ar ded", 10)]
    public UltEvent OnDed;
    public bool hasTrigerd = false;
    public bool triger = false;
    void Update() {
        if (!hasTrigerd) {
            bool IsCoplit = true;
            foreach (SpawnEnemiesTrigger Sponer in NeseserySpanTrigers){
                if (!Sponer.hasSpawnedEnemies || Sponer.enemiesAlive != 0)
                    IsCoplit = false;
            }

            if (IsCoplit || triger) {
                OnDed.Invoke();
                hasTrigerd = true;
            }
        }
    }
}
