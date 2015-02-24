//
//  BusinessCategoryItemStore.h
//  YocoInterviewObjC
//
//  Created by Rohan Jansen on 2015/02/12.
//  Copyright (c) 2015 Rohan Jansen. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BusinessCategory.h"

@class BusinessCategory;

@interface BusinessCategoryItemStore : NSObject

+ (instancetype) sharedStore;
- (BusinessCategory *) createItem: (NSString *)name withCategory:(NSString *)category;
- (NSArray *) allItems;

@property (nonatomic) NSMutableArray *privateItems;

@end
