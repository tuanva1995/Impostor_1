# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)

## [Unreleased]
! wip AttributeBase

- Added: ConstantsSelectionAttribute to popup all const values of a specific type
- Added: InitializationFieldAttribute to make field read-only in Playmode
- Added: MyCursor type to with handy hotspot assignment
- Added: MyCoroutines.CoroutineGroup with handy StartAll() and AnyProcessing
- Added: MyDebug.LogColor(Color) because why not
- Changed: DisplayInspectorAttribute now supports ButtonMethodAttribute inside of displayed types
- Changed: RequiredLayerAttribute might accept layer index instead of the name
- Changed: AnimationStateReference now might reference any object on scene
- Changed: ColliderGizmo now works with MeshColliders
- Extension: MinMax.RandomInRange
- Extension: Transform.StartShake now have "fade" parameter
- Extension: Coroutine.StartNext(IEnumerator) to easilly create sequence of coroutines 
- Extension: SerializedProperty.IsNumerical to detect vectors or float/int
- Extension: SerializedProperty.GetValue/SetValue to operate with object reference
- Fix: Reorderable Collections drawing issue
- Fix: ColliderGizmo compilaton problem in Unity2020.1
- Fix: CleanEmptyDirectories didn't allow to create folders
- Fix: CleanEmptyDirectories nullref exception fix
- Fix: AutoProperty rare mullref exception fix
- Fix: ConditionalField multiple fixes
- Fix: PositiveValeOnlyAttribute label drawing properly
- Fix: GameObject.HasComponent extension redundant constraint removed
- Fix: UnityObjectEditor rare mullref exception fix
- Fix: Billbord component

## [1.3.0] - 2020-01-16
- Added: FoldoutAttribute. Thanks to PixeyeHQ!
- Added: UnityEvent inspector revamp! Now it's foldable and reorderable :). Thanks to Byron Mayne!
- Fix: TransformShakeExtension critical bug fixed

## [1.2.0] - 2019-11-13
- Added: Reorderable Collections!
- Added: Transform.StartShake and Transform.EndShake extension methods. Use on Camera transform for screen shake effect for instance
- Added: NavMeshPath.GetPointsOnPath extension to split path on evenly distributed points
- Added: MyEditor.CopyToClipboard method. Copy string via script like with Ctrl+C
- Changed: ConditionalFieldAttribute works on custom types inside of collections
- Changed: ConditionalFieldAttribute now works much faster!
- Changed: ColliderGizmo now also highlights NavMeshObstacles
- GUIDComponent updated
- Fix: Compilation error fixed
- Fix: MyBox Updater fixed. Exceptional cases logged with warnings


## [1.1.0] - 2019-09-25
- Added: Commentary component. Add commentaries in inspector;
- Fix: UIRelativePosition fixes;
- Few redundant warnings removed
- Versioning changed to release patches more often without extra warnings

## [1.0.4] - 2019-09-16
- Added: UIRelativePosition type allows to align UI element relative to some other RectTransform with offsets and stuff
- Added: AssetPath and AssetFolderPath types. String wrappers with "Browse" button in inspector. Thanks to Nate Wilson (wilsnat) for the idea
- Changed: ConditionalFieldAttribute now works on fields with custom inspectors! Thanks to Nate Wilson (wilsnat)
- Changed: RangedInt/Float and MinMaxInt/Float now have constructors for static instantiation
- Fix: ConditionalFieldAttribute always hide the field if "compare to" values were not assigned

## [1.0.3] - 2019-09-02
- RequireLayer and RequireTag attributes
- MonoSingleton Type
- Fixed indent issue with nested inspector for MinMaxInt/Float, Optional, OptionalMinMax type
- MySceneBundle is a Tool to transfer data from one scene to another. Thanks to Kaynn-Cahya for this addition!

## [1.0.2] - 2019-08-17
- Fix: breaking problem with MyCoroutines type
- Added: MinMaxInt/Float Clamp and Lerp extension methods
- Added: MinMaxInt/Float Length and MidPoint extension methods
- Now MyBox will automatically check for updates!

## [1.0.1] - 2019-08-15
- Compilation errors fixed
- Removed obsolete warning

## [1.0.0] - 2019-08-13
### First version with Unity Package Manager support.
Let's take it as the first release since now you are able to install MyBox with Package Manager and update it with "Tools/MyBox/Check for updates"
