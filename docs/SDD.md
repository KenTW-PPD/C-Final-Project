# 1. Scope 與核心循環

## S1. 關卡倒數計時

### Specification
- 系統應維護一個關卡倒數時間 `time`，初始為 **30 分鐘**。
- 當 `time > 0` 時，系統每幀以 `Time.deltaTime` 遞減。

### Implementation Trace
- Scenes.Start(): time = 30 * 60;
- Scenes.Update(): time -= Time.deltaTime;

### Acceptance Criteria
- 啟動遊戲後，getTime() 在 1 秒後約減少 1 秒（允許誤差）。
- time 不會在小於 0 後繼續下降（目前實作為 if (time > 0)）。

---

## S2. 分數累加

### Specification
- 擊殺一般怪物或 Boss 時，關卡分數應增加。

### Implementation Trace
- Monsters.DropItem()：死亡時 scene.GetComponent<Scenes>().addScore(1)
- Boss01.DropItem()：死亡時同樣 addScore(1)
- Scenes.addScore(int score)

### Acceptance Criteria
- 任一怪物 Health <= 0 被 Destroy 前，Scenes.Score 必須增加 1。

---

# 2. 生成系統（Spawning）

## S3. 一般怪物生成

### Specification
- 系統以固定時間間隔 spawnFrequency 嘗試生成怪物。
- 若場上怪物數量 amount < limit，則依機率生成指定怪物類型，否則本輪不生成。
- 生成位置落於矩形區域 [-Size.x, Size.x] × [-Size.y, Size.y]。

### Implementation Trace
- Scenes.Spawn() 協程：
  - 依 spawnFrequency wait
  - 檢查 amount < limit
  - 機率選 monstersList[0..2]
  - 位置使用 Size
- 機率：
  - >60：40%
  - >40：20%
  - >20：20%
  - default：20% 不生成

### Acceptance Criteria
- limit 足夠大時，運行數分鐘應觀察到三種怪物都會生成，且生成位置符合邊界。
- 當 amount >= limit 時，連續多輪不應生成怪物（直到 amount 降回可生成）。

### Known Limitation
- amount 只在成功生成後更新，怪物死亡後不會即時反映，可能造成短暫不刷怪。

---

## S4. 刷怪難度隨時間成長

### Specification
- 每 60 秒提高關卡難度等級 level。
- spawnFrequency 逐步降低，最低 0.5 秒。
- limit 隨 level 成長增加。

### Implementation Trace
- Scenes.changeSpawnFrequency()：每 60 秒更新 spawnFrequency、limit，level++。

### Acceptance Criteria
- 60 秒後 spawnFrequency 應小於初始值（除非已達 0.5）。
- 60 秒後 limit 應大於初始值。

---

## S5. 怪物等級隨時間成長

### Specification
- 每 120 秒提升 monsterLevel。
- 新生成怪物屬性需反映更高等級。

### Implementation Trace
- Scenes.MonstersLevelUP()：
  - monstersList 中 prefab level = monsterLevel
  - monsterLevel++
- zombie.Start():
  - Health = 15 + level * 10
  - AttackPower = 2 + level * 2

### Acceptance Criteria
- 2 分鐘後生成的 zombie Health 高於初期生成者。

### Assumption
- monstersList 為 prefab reference，而非場景實例。

---

## S6. Boss 生成

### Specification
- 每 600 秒依 Bosses 清單順序生成 Boss。

### Implementation Trace
- Scenes.SpawnBoss():
  - foreach Boss
  - wait 600s
  - Instantiate
- Start() 尚未啟動此協程。

### Acceptance Criteria
- 啟動後 600 秒生成 Bosses[0]。

### Gap
- 需於 Scenes.Start() 啟動 Boss 協程。

---

# 3. 戰鬥系統

## S7. 武器自動攻擊

### Specification
- 武器依攻速自動攻擊範圍內最近敵人。

### Implementation Trace
- AddWeapon() / Awake(): StartCoroutine(AttackRoutine)
- AttackRoutine():
  - FindNearestMonster
  - wait Weapon.getAttackSpeed()
- Fire(playerPos, targetPos, power)
- power = Character.AttackPower + Weapon.AttackPower

### Acceptance Criteria
- 有敵人時依攻速觸發 Fire。
- 無敵人不攻擊。

---

## S8. 索敵範圍與 Layer

### Specification
- 僅搜尋 enemyLayer。
- 半徑為 Weapon.getAttackRange()。

### Implementation Trace
- Physics2D.OverlapCircleAll(position, range, enemyLayer)

### Acceptance Criteria
- 非 enemyLayer 不列入目標。

---

# 4. 受傷、死亡、掉落

## S9. 角色受傷

### Specification
- 受怪物或子彈攻擊扣血。
- 傷害受 Defense 抵銷。

### Implementation Trace
- Monsters：damage / Defense
- MonstersBullet：damage / Defense

### Acceptance Criteria
- 攻擊力高扣血多。
- Defense 高扣血少。

### Risk
- Defense = 0 需保護。

---

## S10. 角色死亡

### Specification
- Health <= 0 觸發 Game Over。

### Implementation Trace
- Character.Update(): if (Health <= 0) GameOver()

### Acceptance Criteria
- 下一幀必定 GameOver。

---

## S11. 怪物死亡掉落

### Specification
- 分數 +1
- 掉 Exp ×1
- 10% 掉 Chest
- Destroy

### Implementation Trace
- Monsters.DropItem()

### Acceptance Criteria
- Exp 生成，怪物消失。

---

## S12. Boss01 掉落

### Specification
- 分數 +1
- Exp ×30
- Chest ×4
- Destroy

### Acceptance Criteria
- Exp +30、Chest +4。

---

## S13. 燃燒狀態

### Specification
- isFire=true 每秒扣 FireDamage。

### Acceptance Criteria
- Health 每秒下降。

---

# 5. 升級系統

## S14. 經驗與升級

### Specification
- 撿 XP 得經驗。
- 達門檻暫停遊戲並顯示 UI。

### Implementation Trace
- OnTriggerEnter(): LevelUP()
- LevelUP(): Time.timeScale = 0

### Acceptance Criteria
- 升級時遊戲暫停。

---

## S15. 升級選項

### Specification
- 隨機五類能力。
- 成長與 rarity² 成正比。
- 選後即套用。

### Acceptance Criteria
- 屬性確實增加。
