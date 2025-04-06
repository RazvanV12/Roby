using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GenerateLevel 
{
    private static GameObject backGroundImage;
    private static GameObject coin;
    private static Vector3 playerStart;
    private static GameObject player;
    private static GameObject finishFlag;
    
    private static GameObject spikeObstacle;
    private static GameObject bigSpikeObstacle;
    private static GameObject blackCloudFlatObstacle;
    private static GameObject blackCloudRoundObstacle;
    private static GameObject greyCloudFlatObstacle;
    private static GameObject greyCloudRoundObstacle;
    
    private static GameObject patrolEnemy;
    private static GameObject shootingEnemy;
    private static GameObject blueBirdEnemy;
    
    private static GameObject higherJumpPowerUp;
    private static GameObject higherSpeedPowerUp;
    private static GameObject invertedControlsPowerUp;
    private static GameObject doubleJumpPowerUp;
    private static GameObject invertGravityPortal;
    
    private static GameObject spikeDropperTrap;
    private static GameObject swingingMaceTrap;
    private static GameObject launchingSawTrap;
    // length 2, height 3
    private static GameObject fallingGroundTrap;
    
    private static GameObject waterSize1;
    private static GameObject waterSize2;
    private static GameObject waterSize3;
    private static GameObject waterSize4;
    // default tile height is 3
    private static GameObject leftGroundTile;
    private static GameObject middleGroundTile;
    private static GameObject rightGroundTile;
    private static GameObject grasslessGroundx4Tile;
    private static GameObject slopeLeftx4Tile;
    private static GameObject slopeRightx4Tile;
    private static GameObject slopeLeftx5Tile;
    private static GameObject slopeRightx5Tile;
    private static GameObject whiteCloudFlatTile;
    private static GameObject whiteCloudRoundTile;

    private static void SetPlayerStartPosition()
    {
        player.transform.position = playerStart;
    }

    private static void AssignGameElementsPrefabs()
    {
        playerStart = Vector3.down;
        backGroundImage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Background.prefab");
        coin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Coin.prefab");
        player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
        finishFlag = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Flag.prefab");
    }

    private static void AssignObstaclesPrefabs()
    {
        spikeObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Spike.prefab");
        bigSpikeObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BigSpike.prefab");
        blackCloudFlatObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Black Cloud.prefab");
        blackCloudRoundObstacle =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Black Cloud2.prefab");
        greyCloudFlatObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Grey Cloud.prefab");
        greyCloudRoundObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Grey Cloud2.prefab");
    }

    private static void AssignEnemiesPrefabs()
    {
        patrolEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PatrolEnemy.prefab");
        shootingEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ShootingEnemy.prefab");
        blueBirdEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BlueBird.prefab");
    }

    private static void AssignPowerUpsPrefabs()
    {
        higherJumpPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/higher_jump.prefab");
        higherSpeedPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/higher_speed.prefab");
        invertedControlsPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/inverted_controls.prefab");
        doubleJumpPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/double_jump.prefab");
        invertGravityPortal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/InvertGravityPortal.prefab");
    }

    private static void AssignTrapsPrefabs()
    {
        spikeDropperTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/SpikeDropper.prefab");
        swingingMaceTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Swinging Mace.prefab");
        launchingSawTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/LaunchingSaw.prefab");
        fallingGroundTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Falling Ground Trap.prefab");
    }

    private static void AssignMapTilesPrefabs()
    {
        waterSize1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Water x1.prefab");
        waterSize2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Water x2.prefab");
        waterSize3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Water x3.prefab");
        waterSize4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Water x4.prefab");   
        
        leftGroundTile =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Left Ground.prefab");
        rightGroundTile =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Right Ground.prefab");
        middleGroundTile =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Middle Ground.prefab");
        grasslessGroundx4Tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/GrasslessGround - 4 Tiles.prefab");
        slopeLeftx4Tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Slope Left x4.prefab");
        slopeLeftx5Tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Slope Left x5.prefab");
        slopeRightx4Tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Slope Right x4.prefab");
        slopeRightx5Tile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Slope Right x5.prefab");
        whiteCloudFlatTile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cloud.prefab");
        whiteCloudRoundTile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cloud2.prefab");
    }

    private static void AssignPrefabsForMapGeneration()
    {
        AssignGameElementsPrefabs();
        AssignObstaclesPrefabs();
        AssignEnemiesPrefabs();
        AssignPowerUpsPrefabs();
        AssignTrapsPrefabs();
        AssignMapTilesPrefabs();
    }

    private static void GenerateMapTiles()
    {
        var lastLevel = 9;
        var currentTilePosition = Vector3.zero * -2;
        var currentGapBetweenTiles = 0;
    }
}
