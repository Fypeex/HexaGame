using System;
using Game;
using Game.Buildings;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class HexTile : MonoBehaviour
{
    
    private Animator _animator;
    private static readonly int StopGrowShrink = Animator.StringToHash("StopGrowShrink");
    private static readonly int PlayGrowShrink = Animator.StringToHash("PlayGrowShrink");
    private bool _isActive;
    private Building _building;
    private BuildingType _buildingType = BuildingType.EMPTY;
    
    [field: SerializeField] public Resourcetype Type { get; private set; }
    [field: SerializeField] public Material GrassMaterial { get; private set; }
    [field: SerializeField] public Material WaterMaterial { get; private set; }
    [field: SerializeField] public Material OilMaterial { get; private set; }
    [field: SerializeField] public Material StoneMaterial { get; private set; }
    [field: SerializeField] public Material WoodMaterial { get; private set; }
    [field: SerializeField] public Material CoalMaterial { get; private set; }
    [field: SerializeField] public Material GoldMaterial { get; private set; }
    [field: SerializeField] public Material IronMaterial { get; private set; }

    private void OnValidate()
    {
        SetMaterial();
    }

    private void SetMaterial()
    {
        switch (Type)
        {
            case Resourcetype.OIL:
                GetComponent<SpriteRenderer>().material = OilMaterial != null ? OilMaterial : Resources.Load<Material>("Materials/Oil");
                break;
            case Resourcetype.WATER:
                GetComponent<SpriteRenderer>().material = WaterMaterial != null ? WaterMaterial : Resources.Load<Material>("Materials/Water");
                break;
            case Resourcetype.GRASS:
                GetComponent<SpriteRenderer>().material = GrassMaterial != null ? GrassMaterial : Resources.Load<Material>("Materials/Grass");
                break;
            case Resourcetype.STONE:
                GetComponent<SpriteRenderer>().material = StoneMaterial != null ? StoneMaterial : Resources.Load<Material>("Materials/Stone");
                break;
            case Resourcetype.IRON:
                GetComponent<SpriteRenderer>().material = IronMaterial != null ? IronMaterial : Resources.Load<Material>("Materials/Iron");
                break;
            case Resourcetype.COAL:
                GetComponent<SpriteRenderer>().material = CoalMaterial != null ? CoalMaterial : Resources.Load<Material>("Materials/Coal");
                break;
            case Resourcetype.GOLD:
                GetComponent<SpriteRenderer>().material = GoldMaterial != null ? GoldMaterial : Resources.Load<Material>("Materials/Gold");
                break;
            case Resourcetype.WOOD:
                GetComponent<SpriteRenderer>().material = WoodMaterial != null ? WoodMaterial : Resources.Load<Material>("Materials/Wood");
                break;
            default:
               GetComponent<SpriteRenderer>().material = GrassMaterial != null ? GrassMaterial : Resources.Load<Material>("Materials/Grass");
                break;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.ResetTrigger(PlayGrowShrink);
        _animator.ResetTrigger(StopGrowShrink);
    }

    public void OnHexClicked()
    {   if(!GameManager.Instance.IsRunning)
            return;
        if(_isActive)
            GameManager.Instance.ClearActiveTile();
        else
            GameManager.Instance.SetActiveTile(this);
    }
    
    public void SetActive()
    {
        _isActive = true;
        var o = gameObject;
        var position = o.transform.position;
        position = new Vector3(position.x, position.y, -1);
        o.transform.position = position;
        GameManager.Instance.ShowBuildMenu(this);
        _animator.SetTrigger(PlayGrowShrink);
    }
    
    public void SetInactive()
    {
        var o = gameObject;
        var position = o.transform.position;
        position = new Vector3(position.x, position.y, 0);
        o.transform.position = position;
        GameManager.Instance.HideBuildMenu();
        
        _animator.SetTrigger(StopGrowShrink);
        _isActive = false;
    }
    
    public void SetType(Resourcetype type)
    {
        Type = type;
        SetMaterial();
    }
    
    public Building GetBuilding() => _building;
    public void SetBuilding(Building building, BuildingType group)
    {
        _building = building;
        _buildingType = group;
    }
    
    public BuildingType GetBuildingType() => _buildingType;
}