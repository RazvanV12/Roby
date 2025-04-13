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
    private static GameObject coin;
    internal static Vector3 playerStart;
    internal static Vector3 playerEnd;
    private static int gapsInARow;

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
    
    private static void AssignGameElementsPrefabs()
    {
        playerStart = Vector3.down;
        coin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Coin.prefab");
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

    internal static void StartLevelGeneration(int level)
    {
        // To implement
        AssignPrefabsForMapGeneration();
        GenerateMapTiles(level);
    }

    private static void GenerateMapTiles(int seedNumber)
    {
        Object.Instantiate(waterSize4, new Vector3(-5, -4, 0), Quaternion.identity);
        Object.Instantiate(waterSize4, new Vector3(-9, -4, 0), Quaternion.identity);
        Object.Instantiate(waterSize4, new Vector3(-13, -4, 0), Quaternion.identity);
        Random.InitState(seedNumber);
        var mapLength = Random.Range(75, 200);

        (Vector3 Position, GameObject Type, String Category, Int16 Height) leftTile = (Position: new Vector3(-1, -2, 0),
            Type: leftGroundTile, Category: "Ground", Height: 3);
        (Vector3 Position, GameObject Type, String Category, Int16 Height) middleTile = (
            Position: new Vector3(0, -2, 0), Type: middleGroundTile, Category: "Ground", Height: 3);
        (Vector3 Position, GameObject Type, String Category, Int16 Height) rightTile = (Position: new Vector3(1, -2, 0),
            Type: null, Category: "Ground", Height: 3);

        UpdateHeightBasedOnType(ref leftTile);
        Object.Instantiate(leftTile.Type, leftTile.Position, Quaternion.identity);
        UpdateHeightBasedOnType(ref middleTile);
        Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);

        gapsInARow = 0;

        for (var i = 0; i < mapLength; i++)
        {
            leftTile = middleTile;
            middleTile = rightTile;
            rightTile = GenerateRandomTile(middleTile);
            
            if(rightTile.Category == "Gap")
                gapsInARow++;
            else
                gapsInARow = 0;

            if (middleTile is { Category: "Ground", Height: 3 })
            {
                if (leftTile.Category == "Gap")
                {
                    middleTile.Type = rightTile.Category == "Gap" ? middleGroundTile : leftGroundTile;
                }
                else
                {
                    middleTile.Type = rightTile.Category == "Gap" ? rightGroundTile : middleGroundTile;
                }
            }
            
            UpdateHeightBasedOnType(ref middleTile);
            Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
        }
        
        middleTile = rightTile;

        if (middleTile.Category == "Ground")
        {
            if (middleTile.Height == 3)
            {
                rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
                middleTile.Type = middleGroundTile;
            }
            else
            {
                rightTile = (middleTile.Position + Vector3.right, slopeRightx4Tile, "Slope Right", 4);
                UpdateHeightBasedOnType(ref middleTile);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                middleTile = rightTile;
                rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
            }

            UpdateHeightBasedOnType(ref middleTile);
            Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
            UpdateHeightBasedOnType(ref rightTile);
            Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
            playerEnd = rightTile.Position;
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 1, -4, 0), Quaternion.identity);
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 5, -4, 0), Quaternion.identity);
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 9, -4, 0), Quaternion.identity);
            return;
        }

        if (middleTile.Category == "Slope Left")
        {
            if (middleTile.Height == 4)
            {
            }
            else
            {
                rightTile = (middleTile.Position + Vector3.right, slopeRightx5Tile, "Slope Right", 5);
                UpdateHeightBasedOnType(ref middleTile);
                Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
                middleTile = rightTile;
            }

            rightTile = (middleTile.Position + Vector3.right, grasslessGroundx4Tile, "Ground", 4);
            UpdateHeightBasedOnType(ref middleTile);
            Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
            UpdateHeightBasedOnType(ref rightTile);
            Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
            playerEnd = rightTile.Position;
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 1, -4, 0), Quaternion.identity);
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 5, -4, 0), Quaternion.identity);
            Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 9, -4, 0), Quaternion.identity);
            return;
        }

        if (middleTile.Category != "Slope Right") return;
        if (middleTile.Height == 4)
        {
            rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
        }
        else
        {
            rightTile = (middleTile.Position + Vector3.right, slopeRightx4Tile, "Slope Right", 4);
            UpdateHeightBasedOnType(ref middleTile);
            Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
            middleTile = rightTile;
            rightTile = (middleTile.Position + Vector3.right, rightGroundTile, "Ground", 3);
        }

        UpdateHeightBasedOnType(ref middleTile);
        Object.Instantiate(middleTile.Type, middleTile.Position, Quaternion.identity);
        UpdateHeightBasedOnType(ref rightTile);
        Object.Instantiate(rightTile.Type, rightTile.Position, Quaternion.identity);
        playerEnd = rightTile.Position;
        Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 1, -4, 0), Quaternion.identity);
        Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 5, -4, 0), Quaternion.identity);
        Object.Instantiate(waterSize4, new Vector3(playerEnd.x + 9, -4, 0), Quaternion.identity);
    }

    // 0 for gap, 1 for ground with x3 height, 2 for slope left x4 - for Normal Ground of Height 3
    // 10 for ground with 4x height, 11 for slope right x4, 12 for slope left x5, 13 for gap - for Normal Ground of Height 4 
    // 20 for ground with 4x height, 21 for slope right x4 , 22 for slope left x5, 23 for gap - for Slope Left of Height 4
    // 30 for ground with 3x height, 31 for slope left x4, 32 for gap - for Slope Right of Height 4
    // 40 for slope right x5, 41 for gap - for Slope Left of Height 5
    // 50 for slope left x5, 51 for slope right x4, 52 for gap - for Slope Right of Height 5

    //1 for ground with x3 height, 2 for slope left x4, 3 for slope right x4, 4 for slope left x5, 5 for ground with 4x height - for gap
    private static (Vector3 Position, GameObject Type, string Category, short Height) GenerateRandomTile(
        (Vector3 Position, GameObject Type, string Category, short Height) neighbourTile)
    {
        switch (neighbourTile.Category)
        {
            case "Ground" when neighbourTile.Height == 3:
            {
                var randomNumber = Random.Range(0, 3);
                return randomNumber switch
                {
                    0 => (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap",
                        short.MinValue),
                    1 => (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground", 3),
                    2 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx4Tile,
                        Category: "Slope Left", 4),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for ground tile with height 3")
                };
            }
            case "Ground":
            {
                var randomNumber = Random.Range(10, 14);
                return randomNumber switch
                {
                    10 => (Position: neighbourTile.Position + Vector3.right, Type: grasslessGroundx4Tile,
                        Category: "Ground", Height: 4),
                    11 => (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                        Category: "Slope Right", Height: 4),
                    12 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile,
                        Category: "Slope Left", Height: 5),
                    13 => (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap",
                        Height: short.MinValue),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for ground tile with height 4")
                };
            }
            case "Slope Left" when neighbourTile.Height == 4:
            {
                var randomNumber = Random.Range(20, 23);
                return randomNumber switch
                {
                    20 => (Position: neighbourTile.Position + Vector3.right, Type: grasslessGroundx4Tile,
                        Category: "Ground", Height: 4),
                    21 => (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                        Category: "Slope Right", Height: 4),
                    22 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile,
                        Category: "Slope Left", Height: 5),
                    23 => (neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap", short.MinValue),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for slope left tile with height 4")
                };
            }
            case "Slope Left":
            {
                var randomNumber = Random.Range(40, 42);
                return randomNumber switch
                {
                    40 => (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx5Tile,
                        Category: "Slope Right", Height: 5),
                    41 => (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap",
                        Height: short.MinValue),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for slope left tile with height 5")
                };
            }
            case "Slope Right" when neighbourTile.Height == 4:
            {
                var randomNumber = Random.Range(30, 33);
                return randomNumber switch
                {
                    30 => (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground", Height: 3),
                    31 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx4Tile,
                        Category: "Slope Left", Height: 4),
                    32 => (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap",
                        Height: short.MinValue),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for slope right tile with height 4")
                };
            }
            case "Slope Right":
            {
                var randomNumber = Random.Range(50, 53);
                return randomNumber switch
                {
                    50 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile,
                        Category: "Slope Left", Height: 5),
                    51 => (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile,
                        Category: "Slope Right", Height: 4),
                    52 => (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap",
                        Height: short.MinValue),
                    _ => throw new TileGenerationException(
                        "Invalid random number generated for slope right tile with height 5")
                };
            }
            case "Gap":
            {
                var gapChance = gapsInARow switch
                {
                    1 => 0.75f,
                    2 => 0.5f,
                    3 => 0.25f,
                    _ => 0f
                };

                if (Random.value <= gapChance)
                {
                    return (Position: neighbourTile.Position + Vector3.right, Type: waterSize1, Category: "Gap", Height: short.MinValue);
                }
                var randomNumber = Random.Range(1, 6);
                return randomNumber switch
                {
                    1 => (Position: neighbourTile.Position + Vector3.right, Type: null, Category: "Ground", Height: 3),
                    2 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx4Tile, Category: "Slope Left",
                        Height: 4),
                    3 => (Position: neighbourTile.Position + Vector3.right, Type: slopeRightx4Tile, Category: "Slope Right",
                        Height: 4),
                    4 => (Position: neighbourTile.Position + Vector3.right, Type: slopeLeftx5Tile, Category: "Slope Left",
                        Height: 5),
                    5 => (Position: neighbourTile.Position + Vector3.right, Type: grasslessGroundx4Tile, Category: "Ground",
                        Height: 4),
                    _ => throw new TileGenerationException("Invalid random number generated for gap tile")
                };
            }
            default:
                throw new TileGenerationException("Invalid mapping for middle tile");
        }
    }

    private static void UpdateHeightBasedOnType(ref (Vector3 Position, GameObject Type, String Category, Int16 Height) tile)
    {
        switch (tile.Category)
        {
            case "Ground" when tile.Height == 3:
                tile.Position.y = -2;
                return;
            case "Ground" when tile.Height == 4:
                tile.Position.y = 0;
                return;
            case "Slope Left" or "Slope Right" when tile.Height == 4:
            case "Slope Left" or "Slope Right" when tile.Height == 5:
                tile.Position.y = -2;
                return;
            case "Gap":
                tile.Position.y = -4;
                return;
        }
    }
}
