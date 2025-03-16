using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateLevel : MonoBehaviour
{
    private GameObject backGroundImage;
    private GameObject coin;
    private Vector3 playerStart;
    private GameObject player;
    private GameObject finishFlag;
    
    private GameObject spikeObstacle;
    private GameObject bigSpikeObstacle;
    private GameObject blackCloudFlatObstacle;
    private GameObject blackCloudRoundObstacle;
    private GameObject greyCloudFlatObstacle;
    private GameObject greyCloudRoundObstacle;
    
    private GameObject patrolEnemy;
    private GameObject shootingEnemy;
    private GameObject blueBirdEnemy;
    
    private GameObject higherJumpPowerUp;
    private GameObject higherSpeedPowerUp;
    private GameObject invertedControlsPowerUp;
    private GameObject doubleJumpPowerUp;
    private GameObject invertGravityPortal;
    
    private GameObject spikeDropperTrap;
    private GameObject swingingMaceTrap;
    private GameObject launchingSawTrap;
    // length 2, height 3
    private GameObject fallingGroundTrap;
    
    private GameObject waterSize1;
    private GameObject waterSize2;
    private GameObject waterSize3;
    private GameObject waterSize4;
    // default tile height is 3
    private GameObject leftGroundTile;
    private GameObject middleGroundTile;
    private GameObject rightGroundTile;
    private GameObject grasslessGroundx4Tile;
    private GameObject slopeLeftx4Tile;
    private GameObject slopeRightx4Tile;
    private GameObject slopeLeftx5Tile;
    private GameObject slopeRightx5Tile;
    private GameObject whiteCloudFlatTile;
    private GameObject whiteCloudRoundTile;
    
    
    void Start()
    {
        AssignPrefabsForMapGeneration();
        SetPlayerStartPosition();
    }

    private void SetPlayerStartPosition()
    {
        player.transform.position = playerStart;
    }

    private void AssignGameElementsPrefabs()
    {
        playerStart = Vector3.down;
        backGroundImage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Background.prefab");
        coin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Coin.prefab");
        player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
        finishFlag = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Flag.prefab");
    }

    private void AssignObstaclesPrefabs()
    {
        spikeObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Spike.prefab");
        bigSpikeObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BigSpike.prefab");
        blackCloudFlatObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Black Cloud.prefab");
        blackCloudRoundObstacle =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Black Cloud2.prefab");
        greyCloudFlatObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Grey Cloud.prefab");
        greyCloudRoundObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Grey Cloud2.prefab");
    }

    private void AssignEnemiesPrefabs()
    {
        patrolEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PatrolEnemy.prefab");
        shootingEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ShootingEnemy.prefab");
        blueBirdEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/BlueBird.prefab");
    }

    private void AssignPowerUpsPrefabs()
    {
        higherJumpPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/higher_jump.prefab");
        higherSpeedPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/higher_speed.prefab");
        invertedControlsPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/inverted_controls.prefab");
        doubleJumpPowerUp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/double_jump.prefab");
        invertGravityPortal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/InvertGravityPortal.prefab");
    }

    private void AssignTrapsPrefabs()
    {
        spikeDropperTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/SpikeDropper.prefab");
        swingingMaceTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Swinging Mace.prefab");
        launchingSawTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/LaunchingSaw.prefab");
        fallingGroundTrap = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Falling Ground Trap.prefab");
    }

    private void AssignMapTilesPrefabs()
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

    private void AssignPrefabsForMapGeneration()
    {
        AssignGameElementsPrefabs();
        AssignObstaclesPrefabs();
        AssignEnemiesPrefabs();
        AssignPowerUpsPrefabs();
        AssignTrapsPrefabs();
        AssignMapTilesPrefabs();
    }

    private void GenerateMapTiles()
    {
        var lastLevel = 9;
        var currentTilePosition = Vector3.zero * -2;
        var currentGapBetweenTiles = 0;
    }
}
