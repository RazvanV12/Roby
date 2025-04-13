using System;
using Exceptions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class GenerateLevel
{
    private static GameObject backGroundImage;
    private static GameObject coin;
    private static Vector3 playerStart;
    private static Vector3 playerEnd;
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
        blackCloudRoundObstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Black Cloud2.prefab");
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

        leftGroundTile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Left Ground.prefab");
        rightGroundTile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Right Ground.prefab");
        middleGroundTile = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Middle Ground.prefab");
        grasslessGroundx4Tile =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/GrasslessGround - 4 Tiles.prefab");
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

    internal static void StartLevelGeneration(int level)
    {
        // To implement
        var seedNumber = level;
        AssignPrefabsForMapGeneration();
        GenerateMapTiles(seedNumber);
    }

    private static void GenerateMapTiles(int seedNumber)
    {
        Random.InitState(seedNumber);
        var mapLength = Random.Range(75, 200);

        (Vector3 Position, GameObject Type, String Category, Int16 Height) leftTile = (Position: new Vector3(-1, -2, 0),
            Type: leftGroundTile, Category: "Ground", Height: 3);
        (Vector3 Position, GameObject Type, String Category, Int16 Height) middleTile = (
            Position: new Vector3(0, -2, 0), Type: middleGroundTile, Category: "Ground", Height: 3);
        (Vector3 Position, GameObject Type, String Category, Int16 Height) rightTile = (Position: new Vector3(1, -2, 0),
            Type: null, Category: "Ground", Height: 3);

        Object.Instantiate(leftTile.Type, leftTile.Position, Quaternion.identity);
        Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);

        for (var i = 0; i < mapLength; i++)
        {
            leftTile = middleTile;
            middleTile = rightTile;
            rightTile = GenerateRandomTile(middleTile);

            if (middleTile.Category == "Ground")
            {
                if (leftTile.Category == "Gap")
                {
                    if (rightTile.Category == "Gap")
                        middleTile.Type = middleGroundTile;
                    else
                        middleTile.Type = leftGroundTile;
                }
                else
                {
                    if(rightTile.Category == "Gap")
                        middleTile.Type = rightGroundTile;
                    else
                        middleTile.Type = middleGroundTile;
                }
            }
            
            Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
        }
        
        middleTile = rightTile;

        if (middleTile.Category == "Ground")
        {
            if (middleTile.Height == 3)
            {
                rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
                middleTile.Type = middleGroundTile;
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
            }
            else
            {
                rightTile = (middleTile.Position + Vector3.right, slopeRightx4Tile, "Slope Right", 4);
                middleTile.Type = grasslessGroundx4Tile;
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                middleTile = rightTile;
                rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
            }
        }

        if (middleTile.Category == "Slope Left")
        {
            if (middleTile.Height == 4)
            {
                rightTile = (middleTile.Position + Vector3.right, grasslessGroundx4Tile, "Ground", 4);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
            }
            else
            {
                rightTile = (middleTile.Position + Vector3.right, slopeRightx4Tile, "Slope Right", 4);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                middleTile = rightTile;
                rightTile = (middleTile.Position + Vector3.right, grasslessGroundx4Tile, "Ground", 4);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
                
            }
        }
        
        if (middleTile.Category == "Slope Right")
        {
            if (middleTile.Height == 4)
            {
                rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
            }
            else
            {
                rightTile = (middleTile.Position + Vector3.right, slopeRightx4Tile, "Slope Right", 4);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                middleTile = rightTile;
                rightTile = (middleTile.Position + Vector3.right, leftGroundTile, "Ground", 3);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
                playerEnd = rightTile.Position;
                
            }
        }

        // TO DO: Add the last tiles for the finish flag 
        // Think about the fact that the height of the tile's position when generated needs to be modified for different tiles 
        // Can't generate them all on height 0 

    }

    // 0 for gap, 1 for ground with x3 height, 2 for slope left x4 - for Normal Ground of Height 3
    // 10 for ground with 4x height, 11 for slope right x4, 12 for slope left x5, 13 for gap - for Normal Ground of Height 4 
    // 20 for ground with 4x height, 21 for slope right x4 , 22 for slope left x5, 23 for gap - for Slope Left of Height 4
    // 30 for ground with 3x height, 31 for slope left x4, 32 for gap - for Slope Right of Height 4
    // 40 for slope right x5, 41 for gap - for Slope Left of Height 5
    // 50 for slope left x5, 51 for slope right x4, 52 for gap - for Slope Right of Height 5

    // 0 for gap, 1 for ground with x3 height, 2 for slope left x4, 3 for slope right x4, 4 for slope left x5, 5 for slope right x5, 6 for ground with 4x height - for gap
    private static (Vector3 Position, GameObject Type, String Category, Int16 Height) GenerateRandomTile(
        (Vector3 Position, GameObject Type, String Category, Int16 Height) neighbourTile)
    {
        if (neighbourTile.Category == "Ground")
        {
            if (neighbourTile.Height == 3)
            {
                var randomNumber = Random.Range(0, 3);
                switch (randomNumber)
                {
                    case 0:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Gap",
                            Height: Int16.MinValue);
                    case 1:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground",
                            Height: 3);
                    case 2:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx4Tile,
                            Category: "Slope Left", 4);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for ground tile with height 3");
                }
            }
            else
            {
                var randomNumber = Random.Range(10, 14);
                switch (randomNumber)
                {
                    case 10:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground",
                            Height: 4);
                    case 11:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                            Category: "Slope Right", Height: 4);
                    case 12:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile,
                            Category: "Slope Left", Height: 5);
                    case 13:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Gap",
                            Height: Int16.MinValue);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for ground tile with height 4");
                }
            }
        }

        if (neighbourTile.Category == "Slope Left")
        {
            if (neighbourTile.Height == 4)
            {
                var randomNumber = Random.Range(20, 23);
                switch (randomNumber)
                {
                    case 20:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground",
                            Height: 4);
                    case 21:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                            Category: "Slope Right", Height: 4);
                    case 22:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile, Category: "Slope Left",
                            Height: 5);
                    case 23:
                        return (neighbourTile.Position + Vector3.right, Type: null, Category: "Gap", Int16.MinValue);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for slope left tile with height 4");
                }
            }
            else
            {
                var randomNumber = Random.Range(40, 42);
                switch (randomNumber)
                {
                    case 40:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx5Tile,
                            Category: "Slope Right", Height: 5);
                    case 41:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Gap",
                            Height: Int16.MinValue);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for slope left tile with height 5");
                }
            }
        }

        if (neighbourTile.Category == "Slope Right")
        {
            if (neighbourTile.Height == 4)
            {
                var randomNumber = Random.Range(30, 33);
                switch (randomNumber)
                {
                    case 30:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground",
                            Height: 4);
                    case 31:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx4Tile,
                            Category: "Slope Left", Height: 4);
                    case 32:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Gap",
                            Height: Int16.MinValue);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for slope right tile with height 4");
                }
            }
            else
            {
                var randomNumber = Random.Range(50, 53);
                switch (randomNumber)
                {
                    case 50:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile,
                            Category: "Slope Left", Height: 5);
                    case 51:
                        return (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                            Category: "Slope Right", Height: 4);
                    case 52:
                        return (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Gap",
                            Height: Int16.MinValue);
                    default:
                        throw new TileGenerationException(
                            "Invalid random number generated for slope right tile with height 5");
                }
            }
        }
        throw new TileGenerationException("Invalid mapping for middle tile");
    }
}
