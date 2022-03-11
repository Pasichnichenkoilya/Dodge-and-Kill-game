using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    EquipmentSlot equipmentSlot;
    List<Modifier> modifiers;


}
public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Feet
}